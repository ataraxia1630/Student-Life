using DoiSinhVien.Core;
using System.Collections.Generic;
using UnityEngine;

namespace DoiSinhVien.Data
{
    public enum RelicRarity { Common, Rare, Legendary }

    [CreateAssetMenu(fileName = "NewRelic", menuName = "Student Life/Relic Data")]
    public class RelicData : ScriptableObject, IPurchasable
    {
        public string id;
        public string relicName;
        [TextArea] public string description;
        public Sprite icon;
        public int price;
        public RelicRarity rarity;

        public List<RelicEffectData> passiveEffects;

        public string ItemName => relicName;
        public int ItemPrice => price;

        public void OnPurchased()
        {
            PlayerInventory.Instance.AddRelic(this);
        }
    }
}