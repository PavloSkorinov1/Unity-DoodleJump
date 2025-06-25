using UnityEngine;

namespace Core.Managers.Gameplay
{
    public class CollectibleManager : ActionBase
    {

        [SerializeField] private GameObject coinPrefab;

        [Range(0f, 1f)]
        [SerializeField] private float spawnProbability = 0.5f;
        
        [SerializeField] private Vector3 spawnOffset = new Vector3(0, 1f, 0);
        

        protected override void ExecuteAction()
        {
            if (coinPrefab == null)
            {
                Debug.LogWarning("CollectibleManager: Coin Prefab is not assigned");
                return;
            }
            
            float randomValue = Random.value;
            
            if (randomValue < spawnProbability)
            {
                Vector3 spawnPosition = transform.position + spawnOffset;
                Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
