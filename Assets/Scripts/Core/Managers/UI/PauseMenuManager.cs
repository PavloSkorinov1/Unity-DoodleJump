using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Managers.UI
{
    public class PauseMenuManager : MonoBehaviour
    {
       public static PauseMenuManager Instance { get; private set; }

        [Header("UI References")]
        [SerializeField] private GameObject pauseMenuUI;

        [Header("Scene References")]
        [SerializeField] private string mainMenuScene = "MainMenu";

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
    }
}
