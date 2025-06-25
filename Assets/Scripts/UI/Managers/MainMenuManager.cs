using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace UI.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Scene References")]
        [SerializeField] private string mainGameScene = "MainGame";
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button leaderboardsButton;
        [SerializeField] private Button exitButton;

        void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        public void StartNewGame()
        {
            SceneManager.LoadScene(mainGameScene);
        }
        
        public void OpenLeaderboards()
        {
            Debug.Log("Opening Leaderboards (Not yet implemented)...");
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
