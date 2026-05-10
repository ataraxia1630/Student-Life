using TMPro;
using UnityEngine;
using DoiSinhVien.Data;


namespace DoiSinhVien.View
{
    public class CardItemView : ShopItemView
    {
        [SerializeField] private TMP_Text mana;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private SpriteRenderer artwork;
        [SerializeField] private GameObject wrapper;

        private CardData _cardData;

        public void Setup(CardData cardData)
        {
            _cardData = cardData;
            mana.text = cardData.manaCost.ToString();
            title.text = cardData.cardName;
            description.text = cardData.description;

            if (cardData.artwork != null && artwork != null)
            {
                artwork.sprite = cardData.artwork;
            }
        }
    }
}
