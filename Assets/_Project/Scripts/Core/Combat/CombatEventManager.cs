using System.Collections.Generic;
using UnityEngine;
using DoiSinhVien.Data;
using DoiSinhVien.UI;

namespace DoiSinhVien.Core
{
    public class CombatEventManager : MonoBehaviour
    {
        [Header("Event Database")]
        public List<CombatEventData> possibleEvents;

        [Header("UI Reference")]
         public EventPopupUI popupUI;

        private void OnEnable()
        {
            GameEvents.OnTurnStart += RollForTurnStartEvents;
            GameEvents.OnTurnEnd += RollForTurnEndEvents;
        }

        private void OnDisable()
        {
            GameEvents.OnTurnStart -= RollForTurnStartEvents;
            GameEvents.OnTurnEnd -= RollForTurnEndEvents;
        }

        private void RollForTurnStartEvents() => RollEvents(EventTriggerTiming.OnTurnStart);
        private void RollForTurnEndEvents() => RollEvents(EventTriggerTiming.OnTurnEnd);

        private void RollEvents(EventTriggerTiming timing)
        {
            Debug.Log($"[Event System] Bắt đầu quét sự kiện cho thời điểm: {timing}");

            if (CombatManager.Instance == null)
            {
                Debug.LogWarning("[Event System] Hủy quét: CombatManager.Instance đang null!");
                return;
            }
            if (possibleEvents == null || possibleEvents.Count == 0)
            {
                Debug.LogWarning("[Event System] Hủy quét: Danh sách possibleEvents trống không! (Nhớ kéo ScriptableObject vào)");
                return;
            }

            var validEvents = possibleEvents.FindAll(e => e != null && e.triggerTiming == timing);
            Debug.Log($"[Event System] Có {validEvents.Count} sự kiện đúng thời điểm {timing} trong kho.");

            foreach (var evt in validEvents)
            {
                float rollValue = Random.value;
                Debug.Log($"[Event System] Đổ xúc xắc cho sự kiện '{evt.eventName}': Ra số {rollValue} (Cần <= {evt.triggerChance} để trúng)");

                if (rollValue <= evt.triggerChance)
                {
                    Debug.Log($"[Event System] TRÚNG THƯỞNG! Kích hoạt sự kiện: {evt.eventName}");
                    TriggerEvent(evt);
                    break;
                }
            }
        }

        private void TriggerEvent(CombatEventData evt)
        {
            Debug.Log($"[Event System] Bắt đầu gọi UI. Kiểm tra: EventPopupUI.Instance có null không? -> {EventPopupUI.Instance == null}");

            if (EventPopupUI.Instance != null)
            {
                Debug.Log("[Event System] Đã tìm thấy UI. Đang ra lệnh bật Popup...");
                EventPopupUI.Instance.ShowEvent(evt, () =>
                {
                    Debug.Log("[Event] Người chơi đã bấm nút xong. Trả lại lượt cho trận đấu!");
                });
            }
            else
            {
                Debug.LogError("[LỖI CHÍ MẠNG] Không tìm thấy EventPopupUI.Instance! Lệnh bật giao diện bị hủy.");
            }
        }
    }
}