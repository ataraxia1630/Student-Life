using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DoiSinhVien.Data;
using DoiSinhVien.Core;

namespace DoiSinhVien.View
{
    public class ShopRelicView : MonoBehaviour
    {
        [Header("Relic Visuals")]
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Image icon;
        [SerializeField] private Image background;

        [Header("Purchase")]
        [SerializeField] private Button buyButton;
        [SerializeField] private TMP_Text priceText;

        private RelicData _relicData;
        private bool _isSoldOut = false;

        public void Setup(RelicData relicData)
        {
            _relicData = relicData;
            _isSoldOut = false;

            nameText.text = relicData.relicName;
            descriptionText.text = relicData.description;
            priceText.text = $"{relicData.price}k";

            if (relicData.icon != null)
                icon.sprite = relicData.icon;

            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(OnBuyClicked);

            CheckAffordability(PlayerInventory.Instance.currentCredits);
        }

        private void OnBuyClicked()
        {
            if (_isSoldOut || PlayerInventory.Instance == null) return;

            if (PlayerInventory.Instance.SpendCredits(_relicData.price))
            {
                _relicData.OnPurchased();
                MarkAsSold();
            }
        }

        private void MarkAsSold()
        {
            _isSoldOut = true;
            buyButton.interactable = false;
            priceText.text = "ĐÃ MUA";
        }

        private void CheckAffordability(int credits)
        {
            if (_isSoldOut) return;
            buyButton.interactable = credits >= _relicData.price;
        }

        private void OnEnable()
        {
            GameEvents.OnCreditsChanged += CheckAffordability;
        }

        private void OnDisable()
        {
            GameEvents.OnCreditsChanged -= CheckAffordability;
        }
    }
}