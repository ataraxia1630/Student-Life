using UnityEngine;
using System.Collections.Generic;
using DoiSinhVien.Data;

namespace DoiSinhVien.Core
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("Map Settings")]
        public int totalLayers = 5; 
        public int nodesPerLayer = 3; 

        [Header("Databases")]
        public List<EncounterBlueprint> easyBlueprints;
        public EncounterBlueprint bossBlueprint;

        public Dictionary<int, MapNodeData> MapGraph { get; private set; } = new Dictionary<int, MapNodeData>();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void GenerateNewMap()
        {
            MapGraph.Clear();
            int currentId = 0;

            List<List<MapNodeData>> layers = new();

            for (int layer = 0; layer < totalLayers; layer++)
            {
                List<MapNodeData> currentLayerNodes = new();

                int nodeCount = (layer == 0 || layer == totalLayers - 1) ? 1 : nodesPerLayer;

                for (int i = 0; i < nodeCount; i++)
                {
                    NodeType type = DetermineNodeType(layer);
                    MapNodeData newNode = new(currentId, layer, type);

                    if (type == NodeType.Combat) newNode.combatBlueprint = GetRandomEasyBlueprint();
                    else if (type == NodeType.Boss) newNode.combatBlueprint = bossBlueprint;

                    currentLayerNodes.Add(newNode);
                    MapGraph.Add(currentId, newNode);
                    currentId++;
                }
                layers.Add(currentLayerNodes);
            }

            for (int layer = 0; layer < totalLayers - 1; layer++)
            {
                List<MapNodeData> bottomNodes = layers[layer];
                List<MapNodeData> topNodes = layers[layer + 1];

                foreach (var bNode in bottomNodes)
                {
                    int targetCount = Random.Range(1, 3);
                    for (int i = 0; i < targetCount; i++)
                    {
                        var tNode = topNodes[Random.Range(0, topNodes.Count)];
                        if (!bNode.nextNodeIds.Contains(tNode.id))
                        {
                            bNode.nextNodeIds.Add(tNode.id);
                        }
                    }
                }
            }

            Debug.Log($"[MapGenerator] Đã tạo thành công bản đồ với {MapGraph.Count} phòng!");
        }

        private NodeType DetermineNodeType(int layer)
        {
            if (layer == 0) return NodeType.Start;
            if (layer == totalLayers - 1) return NodeType.Boss;

            int rand = Random.Range(0, 100);
            if (rand < 50) return NodeType.Combat;
            if (rand < 70) return NodeType.Event;
            if (rand < 85) return NodeType.Shop;
            return NodeType.Rest;
        }

        private EncounterBlueprint GetRandomEasyBlueprint()
        {
            if (easyBlueprints.Count == 0) return null;
            return easyBlueprints[Random.Range(0, easyBlueprints.Count)];
        }
    }
}