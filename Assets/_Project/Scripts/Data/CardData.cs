using DoiSinhVien.Core;
using UnityEngine;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "NewCard", menuName = "Student Life/Card Data")]
    public class CardData : ScriptableObject, IPurchasable
    {
        [Header("General Information")]
        public string id; 
        public string cardName;
        public int price;

        [TextArea(2, 3)]
        public string description; 

        [TextArea(2, 3)]
        public string flavorText; 

        [Header("Stats")]
        public int manaCost; 
        public CardType type;
        public CardRarity rarity;

        [Header("Mechanics")]
        [Tooltip("Có bị loại bỏ khỏi deck sau khi dùng không?")]
        public bool isExhaust;
        [Tooltip("Reference đến phiên bản nâng cấp của thẻ này")]
        public CardData upgradedVersion; 

        [Header("Visuals")]
        public Sprite artwork;

        [Header("Effects")]
        [Tooltip("Một thẻ bài có thể chứa 1 hoặc nhiều hiệu ứng. Các hiệu ứng sẽ được thi triển tuần tự.")]
        public System.Collections.Generic.List<CardEffectData> effects = new();

        public string ItemName => cardName;
        public int ItemPrice => price;

        public void OnPurchased()
        {
            PlayerInventory.Instance.AddCard(this);
        }
    }
}