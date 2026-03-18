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
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (CombatManager.Instance != null)
            {
                CombatManager.Instance.TryPlayCard(this);
            }
        }
    }
}
