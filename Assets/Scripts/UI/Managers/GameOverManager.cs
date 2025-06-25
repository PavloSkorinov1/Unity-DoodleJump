using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro;
using UI.View;

namespace UI.Managers
{
    public class GameOverManager : MonoBehaviour
    {
        public static GameOverManager Instance { get; private set; }

        [Header("UI References")]
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private TextMeshProUGUI finalScoreText;
        [SerializeField] private TextMeshProUGUI finalDistanceText;
        [Header("Scene References")]
        [SerializeField] private string mainMenuSceneName = "MainMenu";
        [Header("Audio")]
        [SerializeField] private AudioClip gameOverSound;
        [SerializeField] private AudioSource backgroundMusicSource;
        
        private AudioSource _audioSource;

        private bool _isGameOver = false; 

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
            
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                Debug.LogError("GameOverManager: AudioSource component is missing");
                enabled = false;
            }
            Time.timeScale = 1f; 
        }

        void Start()
        {
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(false);
            }
            else
            {
                Debug.LogError("GameOverManager: 'GameOverManager' GameObject is not assigned");
                enabled = false; 
            }
        }
        
        public void ShowGameOver()
        {
            if (_isGameOver) return; 

            _isGameOver = true;
            Time.timeScale = 0f; 
            
            // UI
            if (PauseMenuManager.Instance != null && PauseMenuManager.Instance.IsGamePaused())
            {
                PauseMenuManager.Instance.ResumeGame();
            }
            
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }
            
            if (PlayerTracker.Instance != null)
            {
                if (finalScoreText != null)
                {
                    finalScoreText.text = "Coins: " + PlayerTracker.Instance.GetScore().ToString();
                }
                else
                {
                    Debug.LogWarning("GameOverManager: Final Score Text not assigned");
                }

                if (finalDistanceText != null)
                {
                    finalDistanceText.text = "Distance: " + Mathf.FloorToInt(PlayerTracker.Instance.GetDistance()).ToString() + "m";
                }
                else
                {
                    Debug.LogWarning("GameOverManager: Final Distance Text not assigned");
                }
            }
            else
            {
                Debug.LogWarning("GameOverManager: PlayerTracker instance not found");
            }
            
            // AUDIO
            if (_audioSource != null && gameOverSound != null)
            {
                _audioSource.PlayOneShot(gameOverSound);
            }
            else if (_audioSource == null)
            {
                Debug.LogWarning("GameOverManager: AudioSource is missing");
            }
            else 
            {
                Debug.LogWarning("GameOverManager:AudioClip is not assigned");
            }
            
            if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
            {
                backgroundMusicSource.Stop();
            }
            else if (backgroundMusicSource == null)
            {
                Debug.LogWarning("GameOverManager: Background Music not assigned");
            }
        }
        
        public void RestartGame()
        {
            Time.timeScale = 1f; 
            _isGameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public void ReturnToMainMenu()
        {
            Time.timeScale = 1f; 
            _isGameOver = false;
            SceneManager.LoadScene(mainMenuSceneName);
        }

        public bool IsGameOver()
        {
            return _isGameOver;
        }
    }
}
