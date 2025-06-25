using UnityEngine;
using TMPro;

namespace UI.View
{
    public class PlayerTracker : MonoBehaviour
    {
       public static PlayerTracker Instance { get; private set; }
        [Header("UI References")]
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI distanceText;

        [Header("Player Tracking")]
        [SerializeField] private Transform playerTransform;

        private int _currentScore = 0;
        private float _highestYReached = 0f;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            if (playerTransform == null)
            {
                GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
                if (playerObject != null)
                {
                    playerTransform = playerObject.transform;
                }
                else
                {
                    Debug.LogError("PlayerTracker: Player GameObject not found");
                }
            }
            UpdateScoreDisplay();
            UpdateDistanceDisplay();
        }

        void Update()
        {
            if (playerTransform != null)
            {
                if (playerTransform.position.y > _highestYReached)
                {
                    _highestYReached = playerTransform.position.y;
                    UpdateDistanceDisplay();
                }
            }
        }
        
        public void AddScore(int points)
        {
            _currentScore += points;
            UpdateScoreDisplay();
        }
        
        public int GetScore()
        {
            return _currentScore;
        }
        
        public float GetDistance()
        {
            return _highestYReached;
        }
        
        private void UpdateScoreDisplay()
        {
            if (scoreText != null)
            {
                scoreText.text = "Coins: " + _currentScore.ToString();
            }
        }
        
        private void UpdateDistanceDisplay()
        {
            if (distanceText != null)
            {
                distanceText.text = "Distance: " + Mathf.FloorToInt(_highestYReached).ToString() + "m";
            }
        }
    }
}
