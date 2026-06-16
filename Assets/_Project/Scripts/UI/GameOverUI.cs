using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DoiSinhVien.Core; 

namespace DoiSinhVien.UI
{
    public class GameOverUI : MonoBehaviour
    {
        public GameObject gameOverPanel;
        public Button retryButton;
        public Button mainMenuButton;
        public string mapSceneName = "Map";
        public string mainSceneName = "MainMenuScene";

        //private void OnEnable() => GameEvents.OnCombatLose += ShowGameOver;
        //private void OnDisable() => GameEvents.OnCombatLose -= ShowGameOver;

        private void Start()
        {
            //if (gameOverPanel != null) gameOverPanel.SetActive(false);

            retryButton.onClick.AddListener(RestartCombat);
            mainMenuButton.onClick.AddListener(ReturnToMenu);
        }

        private void ShowGameOver()
        {
            if (gameOverPanel != null) gameOverPanel.SetActive(true);
        }

        private void RestartCombat()
        {
            MapGenerator.Instance.ResetMapGraph();
            SceneManager.LoadScene(mapSceneName);
        }

        private void ReturnToMenu()
        {
            SceneManager.LoadScene(mainSceneName);
        }
    }
}