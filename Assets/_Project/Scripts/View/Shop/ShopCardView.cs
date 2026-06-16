using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DoiSinhVien.Data;
using DoiSinhVien.Core;

namespace DoiSinhVien.View
{
    public class ShopCardView : MonoBehaviour
    {
        [Header("Card Visuals")]
        [SerializeField] private TMP_Text manaText;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Image artwork;
        [SerializeField] private Image background;
        [SerializeField] private GameObject wrapper;

        [Header("Purchase")]
        [SerializeField] private Button buyButton;
        [SerializeField] private TMP_Text priceText;

        private CardData _cardData;
        private bool _isSoldOut = false;

        public void Setup(CardData cardData)
        {
            _cardData = cardData;
            _isSoldOut = false;

            titleText.text = cardData.cardName;
            descriptionText.text = cardData.description;
            manaText.text = cardData.manaCost.ToString();
            priceText.text = $"{cardData.price}k";

            if (cardData.artwork != null)
                artwork.sprite = cardData.artwork;

            ApplyTheme();

            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(OnBuyClicked);

            CheckAffordability(PlayerInventory.Instance.currentCredits);
        }

        private void ApplyTheme()
        {
            if (ThemeManager.Instance == null || ThemeManager.Instance.cardVisuals == null) return;

            var config = ThemeManager.Instance.cardVisuals.GetVisualConfig(_cardData.type);
            if (config.backgroundSprite != null)
                background.sprite = config.backgroundSprite;

            background.color = config.tintColor;
        }

        private void OnBuyClicked()
        {
            if (_isSoldOut || PlayerInventory.Instance == null) return;

            if (PlayerInventory.Instance.SpendCredits(_cardData.price))
            {
                _cardData.OnPurchased();
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
            buyButton.interactable = credits >= _cardData.price;
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