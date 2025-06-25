using Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Managers.UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject leaderboardsUI;
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private LeaderboardDisplayManager _leaderboardDisplayManager;
            
        [Header("Scene References")]
        [SerializeField] private string mainGameScene = "MainGame";
        void Start()
        {
            Initialise();
        }

        private void Initialise()
        {
            if (_leaderboardDisplayManager == null)
            {
                Debug.LogWarning("MainMenuManager: LeaderboardDisplayManager instance not found");
            }
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            if (mainMenuUI != null)
            {
                mainMenuUI.SetActive(true);
            }
            else
            {
                Debug.LogError("MainMenuManager: Main Menu UI is not assigned!", this);
            }

            if (leaderboardsUI != null)
            {
                leaderboardsUI.SetActive(false);
            }
            else
            {
                Debug.LogWarning("MainMenuManager: Leaderboards UI is not assigned", this);
            }
        }

        public void StartNewGame()
        {
            SceneManager.LoadScene(mainGameScene);
        }
        
        public void OpenLeaderboards()
        {
            if (mainMenuUI != null)
            {
                mainMenuUI.SetActive(false);
            }
            
            if (leaderboardsUI != null)
            {
                leaderboardsUI.SetActive(true);
            }
            else
            {
                Debug.LogWarning("MainMenuManager: Leaderboards UI is not assigned");
            }
            
            if (_leaderboardDisplayManager != null)
            {
                _leaderboardDisplayManager.RefreshLeaderboardDisplay();
            }
            
        }
        
        public void CloseLeaderboards()
        {
            if (leaderboardsUI != null)
            {
                leaderboardsUI.SetActive(false); 
            }
            if (mainMenuUI != null)
            {
                mainMenuUI.SetActive(true);
            }
        }
        
        public void ExitGame()
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
