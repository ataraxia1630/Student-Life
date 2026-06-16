using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DoiSinhVien.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public Button startButton;
        public Button quitButton;
        public string mapSceneName = "Map";

        private void Start()
        {
            startButton.onClick.AddListener(StartGame);
            quitButton.onClick.AddListener(QuitGame);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(mapSceneName);
        }

        private void QuitGame()
        {
            Debug.Log("Đã thoát game!");
            Application.Quit();
        }
    }
}