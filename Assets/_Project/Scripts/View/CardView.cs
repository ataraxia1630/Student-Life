using DoiSinhVien.Combat;
using DoiSinhVien.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DoiSinhVien.View
{
    public class CardView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text mana;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private SpriteRenderer artwork;
        [SerializeField] private GameObject wrapper;
        [SerializeField] private SpriteRenderer wrapperBackground;

        public CardInstance LogicCard { get; private set; }
        public void Setup(CardInstance logicCard)
        {
            LogicCard = logicCard;
            mana.text = logicCard.CurrentCost.ToString();
            title.text = logicCard.Data.cardName;
            description.text = logicCard.Data.description; 

            if (logicCard.Data.artwork != null && artwork != null)
            {
                artwork.sprite = logicCard.Data.artwork;
            }

            if (ThemeManager.Instance != null && ThemeManager.Instance.cardVisuals != null && wrapperBackground != null)
            {
                var config = ThemeManager.Instance.cardVisuals.GetVisualConfig(logicCard.Data.type);
                if (config.backgroundSprite != null)
                    wrapperBackground.sprite = config.backgroundSprite;

                wrapperBackground.color = config.tintColor;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (CombatManager.Instance == null) return;

            if (CombatManager.Instance.CurrentState == CombatState.Player_Discarding)
            {
                CombatManager.Instance.DiscardSelectedCard(this);
            }
            else
            {
                CombatManager.Instance.TryPlayCard(this);
            }
        }
    }
}
