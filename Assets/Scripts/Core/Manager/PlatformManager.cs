using System.Collections.Generic;
using UnityEngine;

namespace Core.Manager
{
    public class PlatformManager : MonoBehaviour
    {
        [Header("Platform Prefabs")]
        [SerializeField] private GameObject[] platformPrefabs;
        [SerializeField] private GameObject fallingPlatformPrefab;
        [Range(0f, 1f)]
        [SerializeField] private float fallingPlatformSpawnChance = 0.1f;

        [Header("Generation Area")]
        [SerializeField] private float spawnXMin = -2.5f;
        [SerializeField] private float spawnXMax = 2.5f;
        [SerializeField] private float minYDistanceBetweenPlatforms = 5.5f;
        [SerializeField] private float maxYDistanceBetweenPlatforms = 8.5f;

        [Header("Spawn Control")]
        [SerializeField] private float initialSpawnY = 4f;
        [SerializeField] private int platformsToGenerateOnStart = 6;
        [SerializeField] private float spawnAheadOfPlayerY = 10f;

        [Header("Despawn Control")]
        public float despawnBelowPlayerY = 10f;

        private Transform _playerTransform; 
        private float _lastSpawnY;
        private List<GameObject> _activePlatforms = new List<GameObject>();

        void Start()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                _playerTransform = playerObject.transform;
            }
            else
            {
                enabled = false; 
                return;
            }

            _lastSpawnY = _playerTransform.position.y + initialSpawnY;
            
            for (int i = 0; i < platformsToGenerateOnStart; i++)
            {
                SpawnPlatform();
            }
        }

        void Update()
        {
            if (_playerTransform == null) return;
            if (_playerTransform.position.y + spawnAheadOfPlayerY > _lastSpawnY)
            {
                SpawnPlatform();
            }
            
            CleanUpPlatforms();
        }
        
        private void SpawnPlatform()
        {
            if (platformPrefabs == null || platformPrefabs.Length == 0 && fallingPlatformPrefab == null)
            {
                return;
            }
            GameObject platformToSpawn = null;
            
            if (fallingPlatformPrefab != null && Random.value < fallingPlatformSpawnChance)
            {
                platformToSpawn = fallingPlatformPrefab;
            }
            else if (platformPrefabs != null && platformPrefabs.Length > 0)
            {
                platformToSpawn = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
            }
            
            float randomX = Random.Range(spawnXMin, spawnXMax);
            float randomYOffset = Random.Range(minYDistanceBetweenPlatforms, maxYDistanceBetweenPlatforms);
            float spawnY = _lastSpawnY + randomYOffset;
            Vector3 spawnPosition = new Vector3(randomX, spawnY, 0); // Keep Z at 0 for 2D
            GameObject newPlatform = Instantiate(platformToSpawn, spawnPosition, Quaternion.identity);
            _activePlatforms.Add(newPlatform);
            _lastSpawnY = spawnY;
        }
        
        private void CleanUpPlatforms()
        {
            for (int i = _activePlatforms.Count - 1; i >= 0; i--)
            {
                GameObject platform = _activePlatforms[i];
                if (platform == null) 
                {
                    _activePlatforms.RemoveAt(i);
                    continue;
                }

                if (platform.transform.position.y < _playerTransform.position.y - despawnBelowPlayerY)
                {
                    _activePlatforms.RemoveAt(i);
                    Destroy(platform);
                }
            }
        }
    }
}