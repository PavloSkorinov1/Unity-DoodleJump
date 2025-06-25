using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 
using TMPro;

namespace UI.Managers
{
    public class PauseMenuManager : MonoBehaviour
    {
       public static PauseMenuManager Instance { get; private set; }

        [Header("UI References")]
        [SerializeField] private GameObject pauseMenuUI;

        [Header("Scene References")]
        [SerializeField] private string mainMenuScene = "MainMenu";

        [Header("Audio")]
        public AudioClip buttonClickSound;

        private AudioSource _audioSource;
        private bool isGamePaused = false; 

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
                Debug.LogError("PauseMenuManager: AudioSource component is missing");
                enabled = false;
            }
        }

        void Start()
        {
            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(false);
            }
            else
            {
                Debug.LogError("PauseMenuManager: 'Pause Menu UI' GameObject is not assigned");
                enabled = false; 
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void PauseGame()
        {
            isGamePaused = true;
            Time.timeScale = 0f;
            
            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(true);
            }
        }

        public void ResumeGame()
        {
            isGamePaused = false;
            Time.timeScale = 1f;
            
            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(false);
            }
        }
        
        public void ReturnToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(mainMenuScene);
        }
        
        public bool IsGamePaused()
        {
            return isGamePaused;
        }
        
        public void PlayButtonClickSound()
        {
            if (_audioSource != null && buttonClickSound != null)
            {
                _audioSource.PlayOneShot(buttonClickSound);
            }
            else if (buttonClickSound == null)
            {
                Debug.LogWarning("PauseMenuManager: AudioClip is not assigned!");
            }
        }
    }
}
