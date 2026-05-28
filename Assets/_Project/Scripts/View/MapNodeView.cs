using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DoiSinhVien.Data;
using UnityEngine.SceneManagement;
using DoiSinhVien.Core;
using DoiSinhVien.Config;

namespace DoiSinhVien.UI
{
    public class MapNodeView : MonoBehaviour
    {
        [Header("Data")]
        public MapNodeData nodeData;

        [Header("UI References")]
        public Image iconImage;
        public TextMeshProUGUI nodeText;
        public Button nodeButton;

        [Header("Scene Configuration")]
        [Tooltip("Gõ chính xác tên Scene trong thư mục của bạn")]
        public string combatSceneName = "Combat_Prototype";
        public string shopSceneName = "Shop_Prototype";
        public string restSceneName = "Rest_Prototype";
        public float unclickableAlpha = 0.7f;

        public void Setup(MapNodeData data, MapVisualSettings settings, bool isClickable)
        {
            nodeData = data;

            if (nodeText != null)
            {
                nodeText.text = $"{data.type}\n(ID: {data.id})";
            }

            if (settings != null && iconImage != null)
            {
                var config = settings.GetVisualConfig(data.type);
                if (config.icon != null)
                {
                    iconImage.sprite = config.icon;
                }
                iconImage.color = config.iconColor;
                iconImage.color = new Color(iconImage.color.r,
                                            iconImage.color.g,
                                            iconImage.color.b,
                                            isClickable ? 1f : unclickableAlpha);
            }

            if (nodeButton != null)
            {
                nodeButton.interactable = isClickable;
                nodeButton.onClick.RemoveAllListeners();
                if (isClickable)
                {
                    nodeButton.onClick.AddListener(OnNodeClicked);
                }
            }
        }

        public void OnNodeClicked()
        {
            Debug.Log($"Đang di chuyển tới phòng: {nodeData.type}");

            RunManager.Instance.currentNodeId = nodeData.id;
            RunManager.Instance.currentRoomType = nodeData.type;

            switch (nodeData.type)
            {
                case NodeType.Combat:
                case NodeType.Elite:
                case NodeType.Boss:
                    RunManager.Instance.pendingEncounter = nodeData.combatBlueprint;
                    SceneManager.LoadScene(combatSceneName);
                    break;
                case NodeType.Shop:
                    SceneManager.LoadScene(shopSceneName);
                    break;
                case NodeType.Rest:
                    SceneManager.LoadScene(restSceneName);
                    break;
                case NodeType.Event:
                    Debug.Log("Scene Sự kiện chưa có, tạm thời bỏ qua!");
                    break;
            }
        }
    }
}