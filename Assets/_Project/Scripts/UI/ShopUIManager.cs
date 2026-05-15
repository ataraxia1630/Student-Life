using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DoiSinhVien.Core;
using DoiSinhVien.Data;
using UnityEngine.SceneManagement;
using DoiSinhVien.View;

namespace DoiSinhVien.UI
{
    public enum ShopItemType { Card, Relic, Service }
    public class ShopUIManager : MonoBehaviour
    {
        public List<CardData> cardPool;   
        public List<RelicData> relicPool; 

        public int cardCount = 3;
        public int relicCount = 3;

        public TextMeshProUGUI creditText;
        public Transform itemsContainer;

        public GameObject cardPrefab;
        public GameObject relicPrefab;

        public string mapSceneName = "Map_Prototype";

        private void Start()
        {
            UpdateCreditText(PlayerInventory.Instance != null ? PlayerInventory.Instance.currentCredits : 0);
            GenerateShop();
        }

        private void OnEnable()
        {
            GameEvents.OnCreditsChanged += UpdateCreditText;
        }

        private void OnDisable()
        {
            GameEvents.OnCreditsChanged -= UpdateCreditText;
        }

        private void UpdateCreditText(int currentCredits)
        {
            creditText.text = $"Số dư: {currentCredits}k";
        }

        private void GenerateShop()
        {
            for (int i = 0; i < cardCount; i++)
            {
                if (cardPool.Count > 0)
                {
                    CardData randomCard = cardPool[Random.Range(0, cardPool.Count)];
                    CreateItemView(randomCard, ShopItemType.Card);
                }
            }

            for (int i = 0; i < relicCount; i++)
            {
                if (relicPool.Count > 0)
                {
                    RelicData randomRelic = relicPool[Random.Range(0, relicPool.Count)];
                    CreateItemView(randomRelic, ShopItemType.Relic);
                }
            }

            // 3. Tương lai: Sinh thêm Dịch vụ (Xóa bài/Nâng bài) ở đây bằng cách CreateItemView(serviceData)
        }

        private void CreateItemView(IPurchasable itemData, ShopItemType itemType)
        {
            GameObject itemObj = null;
            if (itemType == ShopItemType.Card)
            {
                itemObj = Instantiate(cardPrefab, itemsContainer);
                cardPrefab.GetComponent<CardItemView>().Setup(itemData);
            }
            else if (itemType == ShopItemType.Relic)
            {
                itemObj = Instantiate(relicPrefab, itemsContainer);
            }
            ShopItemView view = itemObj.GetComponent<ShopItemView>();
            view.Setup(itemData);
        }

        public void LeaveShop()
        {
            SceneManager.LoadScene(mapSceneName);
        }
    }
}