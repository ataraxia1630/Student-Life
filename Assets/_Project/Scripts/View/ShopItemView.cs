using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DoiSinhVien.Data;
using DoiSinhVien.Core;

namespace DoiSinhVien.View
{
    public class ShopItemView : MonoBehaviour
    {
        public TMP_Text itemNameText;
        public TMP_Text itemPriceText;
        public SpriteRenderer itemIcon;
        public Button buyButton;

        private IPurchasable _itemData;
        private bool _isSoldOut = false;

        private void OnEnable()
        {
            GameEvents.OnCreditsChanged += CheckAffordability;
        }

        private void OnDisable()
        {
            GameEvents.OnCreditsChanged -= CheckAffordability;
        }

        public void Setup(IPurchasable itemData)
        {
            _itemData = itemData;
            _isSoldOut = false;

            itemNameText.text = _itemData.ItemName;
            itemPriceText.text = $"{_itemData.ItemPrice}k";

            if (_itemData.ItemIcon != null)
            {
                itemIcon.sprite = _itemData.ItemIcon;
            }

            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(OnBuyButtonClicked);

            if (PlayerInventory.Instance != null)
            {
                CheckAffordability(PlayerInventory.Instance.currentCredits);
            }
        }
 

        private void OnBuyButtonClicked()
        {
            if (_isSoldOut || PlayerInventory.Instance == null) return;

            if (PlayerInventory.Instance.SpendCredits(_itemData.ItemPrice))
            {
                _itemData.OnPurchased();

                _isSoldOut = true;
                buyButton.interactable = false;
                itemPriceText.text = "Sold";
                Debug.Log($"[Shop] Giao dịch thành công: {_itemData.ItemName}");
            }
        }

        private void CheckAffordability(int currentCredits)
        {
            if (_isSoldOut) return; 

            buyButton.interactable = (currentCredits >= _itemData.ItemPrice);
        }
    }
}