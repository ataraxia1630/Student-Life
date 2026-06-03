using DoiSinhVien.Core;
using System.Collections.Generic;
using UnityEngine;

namespace DoiSinhVien.Config
{
    [System.Serializable]
    public struct IntentVisual
    {
        public IntentType intentType;
        public Sprite icon;
        public Color iconColor; 
    }

    [CreateAssetMenu(fileName = "CombatVisualSettings", menuName = "Configs/Combat Visual Settings")]
    public class CombatVisualSettings : ScriptableObject
    {
        public List<IntentVisual> intentConfigs;

        public IntentVisual GetIntentVisual(IntentType type)
        {
            foreach (var config in intentConfigs)
            {
                if (config.intentType == type) return config;
            }
            return new IntentVisual { iconColor = Color.white };
        }
    }
}