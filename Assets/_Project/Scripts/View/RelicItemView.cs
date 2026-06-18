using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using DoiSinhVien.Data;

namespace DoiSinhVien.UI
{
    public class RelicItemView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("UI References")]
        public Image relicImage;

        private RelicData currentData;

        public void Setup(RelicData data)
        {
            currentData = data;
            if (relicImage != null && data != null)
            {
                relicImage.sprite = data.icon;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (currentData == null) return;
            TooltipUI.Instance.Show(currentData.relicName, currentData.description);
            Debug.Log($"[Hover] Đang xem Relic: {currentData.relicName}");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (TooltipUI.Instance != null)
            {
                TooltipUI.Instance.Hide();
            }
        }
    }
}