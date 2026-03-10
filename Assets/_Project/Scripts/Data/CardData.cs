using UnityEngine;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "NewCard", menuName = "Student Life/Card Data")]
    public class CardData : ScriptableObject
    {
        [Header("General Information")]
        public string id; 
        public string cardName; 

        [TextArea(2, 3)]
        public string description; 

        [TextArea(2, 3)]
        public string flavorText; 

        [Header("Stats")]
        public int manaCost; 
        public CardType type;
        public CardRarity rarity;

        [Header("Mechanics")]
        public bool isExhaust; 
        public CardData upgradedVersion; 

        [Header("Visuals")]
        public Sprite artwork; 
    }
}