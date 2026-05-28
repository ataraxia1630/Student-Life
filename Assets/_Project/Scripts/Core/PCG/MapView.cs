using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Data;
using DoiSinhVien.Config;
using DoiSinhVien.UI; 

namespace DoiSinhVien.Core
{
    public class MapView : MonoBehaviour
    {
        [Header("Visual")]
        public MapVisualSettings visualSettings;

        [Header("UI References")]
        public RectTransform mapContainer;
        public GameObject nodePrefab;
        public GameObject linePrefab;

        private void Start()
        {
            if (MapGenerator.Instance.MapGraph.Count == 0)
            {
                Debug.Log("Lần đầu vào game, đang sinh bản đồ mới...");
                MapGenerator.Instance.GenerateNewMap();
            }
            else
            {
                Debug.Log("Đã có dữ liệu bản đồ cũ, giữ nguyên hiện trạng!");
            }
            DrawMap();
        }

        public void DrawMap()
        {
            float width = mapContainer.rect.width;
            float height = mapContainer.rect.height;

            foreach (var layer in MapGenerator.Instance.MapLayers)
            {
                foreach (var node in layer)
                {
                    Vector2 startPos = new Vector2(node.normalizedPosition.x * width, node.normalizedPosition.y * height);

                    foreach (var nextId in node.nextNodeIds)
                    {
                        if (MapGenerator.Instance.MapGraph.TryGetValue(nextId, out MapNodeData targetNode))
                        {
                            Vector2 endPos = new Vector2(targetNode.normalizedPosition.x * width, targetNode.normalizedPosition.y * height);
                            DrawLine(startPos, endPos);
                        }
                    }
                }
            }

            foreach (var layer in MapGenerator.Instance.MapLayers)
            {
                foreach (var node in layer)
                {
                    GameObject obj = Instantiate(nodePrefab, mapContainer);
                    RectTransform rect = obj.GetComponent<RectTransform>();

                    rect.anchorMin = Vector2.zero;
                    rect.anchorMax = Vector2.zero;

                    rect.anchoredPosition = new Vector2(node.normalizedPosition.x * width, node.normalizedPosition.y * height);

                    MapNodeView nodeView = obj.GetComponent<MapNodeView>();
                    if (nodeView != null)
                    {
                        bool isClickable = false;
                        int currentId = RunManager.Instance.currentNodeId;
                        if (MapGenerator.Instance.MapGraph.TryGetValue(currentId, out MapNodeData currentNode))
                        {
                            if (currentNode.nextNodeIds.Contains(node.id))
                            {
                                isClickable = true;
                            }
                        }
                        nodeView.Setup(node, visualSettings, isClickable);
                    }
                }
            }
        }

        private void DrawLine(Vector2 startA, Vector2 endB)
        {
            GameObject lineObj = Instantiate(linePrefab, mapContainer);
            RectTransform lineRect = lineObj.GetComponent<RectTransform>();

            lineRect.anchorMin = Vector2.zero;
            lineRect.anchorMax = Vector2.zero;

            Vector2 dir = endB - startA;
            float distance = dir.magnitude;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            lineRect.anchoredPosition = startA;
            lineRect.sizeDelta = new Vector2(distance, lineRect.sizeDelta.y);
            lineRect.localRotation = Quaternion.Euler(0, 0, angle);

            lineRect.SetAsFirstSibling();
        }
    }
}