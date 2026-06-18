using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

namespace DoiSinhVien.UI
{
    public class TooltipUI : MonoBehaviour
    {
        public static TooltipUI Instance { get; private set; }

        [Header("UI References")]
        public GameObject tooltipPanel; 
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI descriptionText;

        private RectTransform rectTransform;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            rectTransform = tooltipPanel.GetComponent<RectTransform>();
            Hide(); 
        }

        private void Update()
        {
            if (tooltipPanel.activeSelf)
            {
                Vector2 mousePos = Vector2.zero;

                if (Mouse.current != null)
                {
                    mousePos = Mouse.current.position.ReadValue();
                }

                mousePos.x += 15f;
                mousePos.y -= 15f;

                rectTransform.position = mousePos;
            }
        }

        public void Show(string title, string description)
        {
            titleText.text = title;
            descriptionText.text = description;
            tooltipPanel.SetActive(true);

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }

        public void Hide()
        {
            tooltipPanel.SetActive(false);
        }
    }
}