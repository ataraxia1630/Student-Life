using DoiSinhVien.Data;
using System.Collections.Generic;
using UnityEngine;

namespace DoiSinhVien.Config
{
    [System.Serializable]
    public struct NodeVisualConfig
    {
        public NodeType nodeType; 
        public Sprite icon;
        public Color iconColor;
    }

    [CreateAssetMenu(fileName = "NewMapVisualSettings", menuName = "Configs/Map Visual Settings")]
    public class MapVisualSettings : ScriptableObject
    {
        public List<NodeVisualConfig> nodeConfigs;

        public NodeVisualConfig GetVisualConfig(NodeType type)
        {
            foreach (var config in nodeConfigs)
            {
                if (config.nodeType == type) return config;
            }
            return new NodeVisualConfig { iconColor = Color.white };
        }
    }
}