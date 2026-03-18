using DoiSinhVien.Combat;
using TMPro;
using UnityEngine;

namespace DoiSinhVien.View
{
    public class CardView : MonoBehaviour
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

    }
}
