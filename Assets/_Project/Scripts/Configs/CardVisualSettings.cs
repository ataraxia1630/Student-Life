using DoiSinhVien.Data;
using System.Collections.Generic;
using UnityEngine;

namespace DoiSinhVien.Config
{
    [System.Serializable]
    public struct CardTypeVisual
    {
        public CardType type;
        public Sprite backgroundSprite; 
        public Color tintColor;        
    }

    [CreateAssetMenu(fileName = "CardVisualSettings", menuName = "Configs/Card Visual Settings")]
    public class CardVisualSettings : ScriptableObject
    {
        public List<CardTypeVisual> typeConfigs;

        public CardTypeVisual GetVisualConfig(CardType type)
        {
            foreach (var config in typeConfigs)
            {
                if (config.type == type) return config;
            }
            return new CardTypeVisual { tintColor = Color.white }; 
        }
    }
}