using System.Collections.Generic;
using UnityEngine;

namespace DoiSinhVien.Data
{
    public enum NodeType
    {
        Start,    
        Combat,   
        Elite,   
        Event,    
        Shop,     
        Rest,     
        Boss     
    }

    [System.Serializable]
    public class MapNodeData
    {
        public int id;             
        public int layer;         
        public NodeType type;      

        public List<int> nextNodeIds = new();

        public EncounterBlueprint combatBlueprint;

        public Vector2 normalizedPosition;

        public MapNodeData(int id, int layer, NodeType type)
        {
            this.id = id;
            this.layer = layer;
            this.type = type;
        }
    }
}