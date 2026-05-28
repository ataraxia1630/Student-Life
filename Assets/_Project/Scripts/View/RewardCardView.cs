using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DoiSinhVien.Data;
using DoiSinhVien.Combat;
using DoiSinhVien.Core;

namespace DoiSinhVien.View
{
    public class RewardCardView : MonoBehaviour, IPointerClickHandler
    {
        [Header("UI References")]
        public TMP_Text titleText;
        public TMP_Text manaText;
        public TMP_Text descriptionText;
        public Image artworkImage;
        public Image backgroundImage;

        private CardData _cardData;

        public void Setup(CardData data)
        {
            _cardData = data;

            if (titleText != null) titleText.text = data.cardName;
            if (descriptionText != null) descriptionText.text = data.description;
            if (manaText != null) manaText.text = data.manaCost.ToString();
            if (artworkImage != null && data.artwork != null) artworkImage.sprite = data.artwork;

            if (ThemeManager.Instance != null && ThemeManager.Instance.cardVisuals != null && backgroundImage != null)
            {
                var config = ThemeManager.Instance.cardVisuals.GetVisualConfig(data.type);
                if (config.backgroundSprite != null)
                    backgroundImage.sprite = config.backgroundSprite;

                backgroundImage.color = config.tintColor;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            RewardManager.Instance.ClaimCard(_cardData);
        }
    }
}