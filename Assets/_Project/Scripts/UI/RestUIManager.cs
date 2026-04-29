using UnityEngine;
using TMPro;
using DoiSinhVien.Core;
using UnityEngine.SceneManagement;

namespace DoiSinhVien.UI
{
    public class RestUIManager : MonoBehaviour
    {
        [Header("UI Panels")]
        public GameObject choicePanel;      
        public GameObject cardSelectPanel;  

        [Header("UI Texts")]
        public TextMeshProUGUI statusText;  

        [Header("Scene Settings")]
        public string mapSceneName = "Map"; 

        private void Start()
        {
            choicePanel.SetActive(true);
            cardSelectPanel.SetActive(false);
            UpdateStatusText();
        }

        private void UpdateStatusText()
        {
            if (RunManager.Instance != null)
            {
                statusText.text = $"Tinh thần: {PlayerInventory.Instance.currentHealth} / {PlayerInventory.Instance.maxHealth}";
            }
        }

        public void OnRestClicked()
        {
            if (PlayerInventory.Instance == null) return;

            int healAmount = Mathf.RoundToInt(PlayerInventory.Instance.maxHealth * 0.3f);

            PlayerInventory.Instance.currentHealth = Mathf.Min(
                PlayerInventory.Instance.currentHealth + healAmount,
                PlayerInventory.Instance.maxHealth
            );

            Debug.Log($"[Quán Cafe] Đã chợp mắt một lúc. Hồi {healAmount} Tinh thần!");
            UpdateStatusText();

            ReturnToMap();
        }

        public void OnUpgradeClicked()
        {
            choicePanel.SetActive(false);
            cardSelectPanel.SetActive(true);

            Debug.Log("[Quán Cafe] Đang mở danh sách Master Deck để ôn luyện...");

            // TODO: Ở bản hoàn thiện, ta sẽ code một vòng lặp instantiate (đẻ) các CardView 
            // từ RunManager.Instance.masterDeck ra màn hình để người chơi click chọn.
        }

        public void SimulateUpgradeCard()
        {
            if (RunManager.Instance != null && PlayerInventory.Instance.masterDeck.Count > 0)
            {
                var cardToUpgrade = PlayerInventory.Instance.masterDeck[0];
                Debug.Log($"[Quán Cafe] Đã nâng cấp thành công thẻ: {cardToUpgrade.cardName}!");
            }
            else
            {
                Debug.LogWarning("Master Deck đang trống, không có gì để nâng cấp!");
            }

            ReturnToMap();
        }

        public void ReturnToMap()
        {
            Debug.Log("Rời khỏi quán, chuẩn bị mở Map...");
            SceneManager.LoadScene(mapSceneName);
        }
    }
}