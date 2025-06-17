using TMPro;
using UnityEngine;
using TMPro;

namespace Core.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject gameElementsParent;
        [SerializeField] private GameObject startingBackground;
        [SerializeField] private TextMeshProUGUI startScreenText; 
        [SerializeField] private TextMeshProUGUI scoreText;

        private bool isGameStarted = false;
        private int currentScore = 0; 

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
            if (gameElementsParent != null)
            {
                gameElementsParent.SetActive(false);
            }
            else
            {
                Debug.LogError("GameManager: 'Game Elements Parent' not assigned");
                enabled = false;
                return;
            }
            
            if (startScreenText != null)
            {
                startScreenText.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("GameManager: 'Start Screen Text' not assigned");
            }
            
            if (startingBackground != null)
            {
                startingBackground.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("GameManager: 'Starting Background' not assigned");
            }
            UpdateScoreDisplay();
        }

        void Update()
        {
            if (!isGameStarted)
            {
                if (Input.anyKeyDown)
                {
                    StartGame();
                }
            }
        }

        public void StartGame()
        {
            isGameStarted = true;
            if (gameElementsParent != null)
            {
                gameElementsParent.SetActive(true);
            }

            if (startScreenText != null)
            {
                startScreenText.gameObject.SetActive(false);
            }

            if (scoreText != null)
            {
                scoreText.gameObject.SetActive(true);
            }
            
            if (startingBackground != null)
            {
                startingBackground.SetActive(false);
            }
        }

        public void AddScore(int points)
        {
            currentScore += points;
            UpdateScoreDisplay();
        }
        private void UpdateScoreDisplay()
        {
            if (scoreText != null)
            {
                scoreText.text = "Coins: " + currentScore.ToString();
            }
        }
    }
}


