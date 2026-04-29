using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Data;

namespace DoiSinhVien.UI
{
    public class MapUIManager : MonoBehaviour
    {
        public GameObject mapNodePrefab; 
        public Transform mapContainer;   

        private void Start()
        {
            if (RunManager.Instance.CurrentMap.Count == 0 && RunManager.Instance.mapGenerator != null)
            {
                RunManager.Instance.mapGenerator.GenerateNewMap();
            }

            DrawMap();
        }

        private void DrawMap()
        {
            var mapGraph = RunManager.Instance.CurrentMap;

            foreach (var kvp in mapGraph)
            {
                MapNodeData node = kvp.Value;

                GameObject nodeObj = Instantiate(mapNodePrefab, mapContainer);
                MapNodeView nodeView = nodeObj.GetComponent<MapNodeView>();

                nodeView.Setup(node);

                
            }
        }
    }
}