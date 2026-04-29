using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DoiSinhVien.Data;
using UnityEngine.SceneManagement;
using DoiSinhVien.Core;

namespace DoiSinhVien.UI
{
    public class MapNodeView : MonoBehaviour
    {
        public MapNodeData nodeData;
        public TextMeshProUGUI nodeText;
        public Button nodeButton;

        public void Setup(MapNodeData data)
        {
            nodeData = data;
            nodeText.text = $"{data.type}\n(ID: {data.id})"; 
            nodeButton.onClick.AddListener(OnNodeClicked);
        }

        public void OnNodeClicked()
        {
            Debug.Log($"Đang di chuyển tới phòng: {nodeData.type}");

            RunManager.Instance.currentNodeId = nodeData.id;

            switch (nodeData.type)
            {
                case NodeType.Combat:
                case NodeType.Elite:
                case NodeType.Boss:
                    RunManager.Instance.pendingEncounter = nodeData.combatBlueprint;
                    SceneManager.LoadScene("CombatPrototype"); 
                    break;
                case NodeType.Shop:
                    SceneManager.LoadScene("ShopPrototype");
                    break;
                case NodeType.Rest:
                    SceneManager.LoadScene("RestPrototype");
                    break;
                case NodeType.Event:
                    Debug.Log("Scene Sự kiện chưa có, tạm thời bỏ qua!");
                    break;
            }
        }
    }
}