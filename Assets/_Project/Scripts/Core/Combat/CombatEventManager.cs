using System.Collections.Generic;
using System.Linq;
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

        // Bộ nhớ tạm trong 1 trận đấu
        private HashSet<CombatEventData> triggeredEvents = new();
        private Dictionary<string, int> cardPlayTracker = new();
        private int cardsPlayedThisTurn = 0;

        private void OnEnable()
        {
            GameEvents.OnCombatStart += ResetEventMemory;
            GameEvents.OnTurnStart += RollForTurnStartEvents;
            GameEvents.OnTurnEnd += RollForTurnEndEvents;
            GameEvents.OnCardPlayed += RollForCardPlayedEvents;
        }

        private void OnDisable()
        {
            GameEvents.OnCombatStart -= ResetEventMemory;
            GameEvents.OnTurnStart -= RollForTurnStartEvents;
            GameEvents.OnTurnEnd -= RollForTurnEndEvents;
            GameEvents.OnCardPlayed -= RollForCardPlayedEvents;
        }

        private void ResetEventMemory()
        {
            triggeredEvents.Clear();
            cardPlayTracker.Clear();
            cardsPlayedThisTurn = 0;
        }

        private void RollForTurnStartEvents()
        {
            cardsPlayedThisTurn = 0; 
            RollEvents(EventTriggerTiming.OnTurnStart);
        }
        private void RollForTurnEndEvents() => RollEvents(EventTriggerTiming.OnTurnEnd);

        private void RollForCardPlayedEvents(Combat.CardInstance card)
        {
            cardsPlayedThisTurn++;

            // Lưu lịch sử đánh bài cho Event 05
            string cardId = card.Data.id;
            if (!cardPlayTracker.ContainsKey(cardId)) cardPlayTracker[cardId] = 0;
            cardPlayTracker[cardId]++;

            RollEvents(EventTriggerTiming.OnCardPlayed, card);
        }

        private void RollEvents(EventTriggerTiming timing, Combat.CardInstance playedCard = null)
        {
            if (CombatManager.Instance == null || possibleEvents == null) return;

            // Lọc các sự kiện: Đúng thời điểm + Chưa từng kích hoạt + Đủ điều kiện Logic
            var validEvents = possibleEvents.FindAll(e =>
                e != null &&
                e.triggerTiming == timing &&
                !triggeredEvents.Contains(e) &&
                CheckConditions(e, playedCard));

            foreach (var evt in validEvents)
            {
                if (Random.value <= evt.triggerChance)
                {
                    triggeredEvents.Add(evt); // Đánh dấu đã chạy

                    Debug.Log($"[Event System] POPUP EVENT Kích hoạt: {evt.eventName}");
                    TriggerPopupEvent(evt);
                    break; // Chỉ chạy 1 event mỗi lần để tránh kẹt UI
                }
            }
        }

        // BỘ LỌC ĐIỀU KIỆN SIÊU CẤP
        private bool CheckConditions(CombatEventData evt, Combat.CardInstance playedCard)
        {
            var combat = CombatManager.Instance;
            var player = combat.player;

            // 1. Kiểm tra Lượt (Turn)
            if (evt.exactTurn > 0 && combat.currentTurn != evt.exactTurn) return false;
            if (evt.minTurn > 0 && combat.currentTurn < evt.minTurn) return false;

            // 2. Kiểm tra Máu Player
            if (evt.maxPlayerHpPercent < 1f)
            {
                float hpPct = (float)player.CurrentHealth / player.MaxHealth;
                if (hpPct > evt.maxPlayerHpPercent) return false;
            }

            // 3. Kiểm tra Máu Quái (Áp dụng cho Event 02)
            if (evt.maxEnemyHpPercent < 1f)
            {
                bool hasLowHpEnemy = combat.activeEnemies.Any(e => e.CurrentHealth > 0 &&
                                    ((float)e.CurrentHealth / e.data.maxHealth) <= evt.maxEnemyHpPercent);
                if (!hasLowHpEnemy) return false;
            }

            // 4. Kiểm tra Status (Áp dụng cho Event 08)
            if (evt.requiredStatus != null)
            {
                if (!player.ActiveStatuses.Keys.Any(s => s == evt.requiredStatus)) return false;
            }

            // 5. Kiểm tra Loại Trận Combat (Áp dụng cho Event 11)
            //if (evt.requireEliteCombat && !combat.isEliteCombat) return false;

            // 6. Kiểm tra Thẻ bài vừa đánh (Áp dụng cho Event 05, Event 10)
            //if (evt.triggerTiming == EventTriggerTiming.OnCardPlayed && playedCard != null)
            //{
            //    if (evt.requiredCardType != CardType.None && playedCard.Data.type != evt.requiredCardType) return false;

            //    if (!string.IsNullOrEmpty(evt.requiredCardId))
            //    {
            //        if (playedCard.Data.id != evt.requiredCardId) return false;
            //        if (evt.requiredCardPlayCount > 0 && cardPlayTracker[evt.requiredCardId] < evt.requiredCardPlayCount) return false;
            //    }
            //}

            return true;
        }

        private void TriggerPopupEvent(CombatEventData evt)
        {
            if (EventPopupUI.Instance != null)
            {
                EventPopupUI.Instance.ShowEvent(evt, () => {
                    Debug.Log("[Event] Người chơi đã giải quyết xong sự kiện.");
                });
            }
        }
    }
}