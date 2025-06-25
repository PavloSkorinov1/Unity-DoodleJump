using UI.View;
using UnityEngine;

namespace Core.Managers.UI
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game Elements")]
        [SerializeField] private GameObject gameElementsParent;

        // private bool isGameStarted = false;

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
            if (SetDefaultConditions()) return;
        }

        private bool SetDefaultConditions()
        {
            // isGameStarted = true;
            if (gameElementsParent != null)
            {
                gameElementsParent.SetActive(true);
            }
            else
            {
                Debug.LogError("GameManager: 'Game Elements Parent' not assigned");
                enabled = false;
                return true;
            }
            
            if (PlayerTracker.Instance != null)
            {
                if (PlayerTracker.Instance.scoreText != null)
                {
                    PlayerTracker.Instance.scoreText.gameObject.SetActive(true);
                }
                if (PlayerTracker.Instance.distanceText != null)
                {
                    PlayerTracker.Instance.distanceText.gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogWarning("GameManager: ScoreAndDistanceTracker instance not found. Score/Distance UI might not be visible.");
            }
            return false;
        }
    }
}


