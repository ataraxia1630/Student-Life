using UnityEngine;
using System.Collections.Generic;
using DoiSinhVien.Data;
using System.Linq;

namespace DoiSinhVien.Core
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("Map Settings")]
        public int totalLayers = 5; 
        public int nodesPerLayer = 3; 

        [Header("Databases")]
        public List<EncounterBlueprint> easyBlueprints;
        public List<EncounterBlueprint> eliteBlueprints;
        public EncounterBlueprint bossBlueprint;

        public Dictionary<int, MapNodeData> MapGraph { get; private set; } = new Dictionary<int, MapNodeData>();

        public List<List<MapNodeData>> MapLayers { get; private set; } = new();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void GenerateNewMap()
        {
            MapGraph.Clear();
            MapLayers.Clear();
            int currentId = 0;

            for (int layer = 0; layer < totalLayers; layer++)
            {
                List<MapNodeData> currentLayerNodes = new();
                int nodeCount = (layer == 0 || layer == totalLayers - 1) ? 1 : nodesPerLayer;

                for (int i = 0; i < nodeCount; i++)
                {
                    NodeType type = DetermineNodeType(layer);
                    MapNodeData newNode = new(currentId, layer, type);

                    if (type == NodeType.Combat)
                        newNode.combatBlueprint = GetRandomBlueprint(layer, easyBlueprints);
                    else if (type == NodeType.Elite)
                        newNode.combatBlueprint = GetRandomBlueprint(layer, eliteBlueprints);
                    else if (type == NodeType.Boss)
                        newNode.combatBlueprint = bossBlueprint;

                    float xPos = (i + 1f) / (nodeCount + 1f) + Random.Range(-0.05f, 0.05f);
                    float yPos = (float)layer / (totalLayers - 1) + Random.Range(-0.02f, 0.02f);
                    newNode.normalizedPosition = new Vector2(xPos, yPos);

                    currentLayerNodes.Add(newNode);
                    MapGraph.Add(currentId, newNode);
                    currentId++;
                }
                MapLayers.Add(currentLayerNodes);
            }

            GenerateEdges();

            Debug.Log($"[MapGenerator] Đã tạo thành công bản đồ với {MapGraph.Count} phòng!");
        }

        private void GenerateEdges()
        {
            for (int layer = 0; layer < totalLayers - 1; layer++)
            {
                List<MapNodeData> bottomNodes = MapLayers[layer];
                List<MapNodeData> topNodes = MapLayers[layer + 1];

                List<Vector2Int> edges = new List<Vector2Int>();

                // TRƯỜNG HỢP 1: Tầng Start (Nối tỏa ra TẤT CẢ các phòng ở tầng 1)
                if (layer == 0)
                {
                    for (int t = 0; t < topNodes.Count; t++)
                        bottomNodes[0].nextNodeIds.Add(topNodes[t].id);
                    continue;
                }

                // TRƯỜNG HỢP 2: Tầng áp chót (TẤT CẢ tự động chụm vào phòng Boss)
                if (layer == totalLayers - 2)
                {
                    for (int b = 0; b < bottomNodes.Count; b++)
                        bottomNodes[b].nextNodeIds.Add(topNodes[0].id);
                    continue;
                }

                // TRƯỜNG HỢP 3: Các tầng ở giữa
                // Bước 1: Nối thẳng hàng
                for (int i = 0; i < bottomNodes.Count; i++)
                {
                    int tIndex = Mathf.Clamp(Mathf.RoundToInt((float)i * (topNodes.Count - 1) / Mathf.Max(1, bottomNodes.Count - 1)), 0, topNodes.Count - 1);
                    edges.Add(new Vector2Int(i, tIndex));
                }

                // Bước 2: Chữa lỗi "Mồ côi" (Node trên không có ai nối tới)
                for (int j = 0; j < topNodes.Count; j++)
                {
                    bool hasIncoming = edges.Exists(e => e.y == j);
                    if (!hasIncoming)
                    {
                        int bIndex = Mathf.Clamp(Mathf.RoundToInt((float)j * (bottomNodes.Count - 1) / Mathf.Max(1, topNodes.Count - 1)), 0, bottomNodes.Count - 1);
                        edges.Add(new Vector2Int(bIndex, j));
                    }
                }

                // Bước 3: Rải rác thêm nhánh ngang (Có kiểm tra chống vắt chéo)
                for (int i = 0; i < bottomNodes.Count; i++)
                {
                    if (Random.value < 0.35f) // Tỷ lệ sinh thêm nhánh
                    {
                        int currentTarget = edges.Find(e => e.x == i).y;
                        int offset = Random.value > 0.5f ? 1 : -1;
                        int newTarget = currentTarget + offset;

                        if (newTarget >= 0 && newTarget < topNodes.Count)
                        {
                            bool isCrossing = false;
                            foreach (var edge in edges)
                            {
                                if (edge.x < i && edge.y > newTarget) isCrossing = true;
                                if (edge.x > i && edge.y < newTarget) isCrossing = true;
                            }

                            bool isDuplicate = edges.Exists(e => e.x == i && e.y == newTarget);

                            if (!isCrossing && !isDuplicate)
                            {
                                edges.Add(new Vector2Int(i, newTarget));
                            }
                        }
                    }
                }

                // Bước 4: Áp dụng vào Data
                foreach (var edge in edges)
                {
                    bottomNodes[edge.x].nextNodeIds.Add(topNodes[edge.y].id);
                }
            }
        }

        private NodeType DetermineNodeType(int layer)
        {
            if (layer == 0) return NodeType.Start;
            if (layer == totalLayers - 1) return NodeType.Boss;

            int rand = Random.Range(0, 100);
            if (layer == 1)
            {
                if (rand < 50) return NodeType.Combat;
                if (rand < 75) return NodeType.Event;
                if (rand < 90) return NodeType.Shop;
                return NodeType.Rest;
            }

            if (rand < 35) return NodeType.Combat;
            if (rand < 50) return NodeType.Elite;
            if (rand < 70) return NodeType.Event;
            if (rand < 85) return NodeType.Shop;
            return NodeType.Rest;
        }

        private EncounterBlueprint GetRandomEasyBlueprint()
        {
            if (easyBlueprints.Count == 0) return null;
            return easyBlueprints[Random.Range(0, easyBlueprints.Count)];
        }

        private EncounterBlueprint GetRandomBlueprint(int currentLayer, List<EncounterBlueprint> pool)
        {
            if (pool == null || pool.Count == 0) return null;

            var validBlueprints = pool.Where(b => b.targetFloor == currentLayer).ToList();

            int fallbackLayer = currentLayer - 1;
            while (validBlueprints.Count == 0 && fallbackLayer >= 0)
            {
                validBlueprints = pool.Where(b => b.targetFloor == fallbackLayer).ToList();
                fallbackLayer--;
            }

            if (validBlueprints.Count == 0)
            {
                validBlueprints = pool;
            }

            return validBlueprints[Random.Range(0, validBlueprints.Count)];
        }
    }
}