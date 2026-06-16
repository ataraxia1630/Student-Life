using System.Collections.Generic;
using UnityEngine;

namespace DoiSinhVien.Data
{
    public enum EventTriggerTiming { OnTurnStart, OnTurnEnd, OnCardPlayed, OnEnemyIntentSet }

    [System.Serializable]
    public class EventChoice
    {
        [Tooltip("Dòng chữ hiện trên nút bấm")]
        public string choiceText;

        [Tooltip("Các hậu quả sẽ xảy ra khi bấm nút này")]
        public List<EventOutcomeData> outcomes;
    }

    [CreateAssetMenu(fileName = "NewCombatEvent", menuName = "Student Life/Combat Event Data")]
    public class CombatEventData : ScriptableObject
    {
        public string eventName;
        [TextArea] public string description;

        [Header("Trigger Timing")]
        public EventTriggerTiming triggerTiming;
        [Range(0f, 1f)] public float triggerChance = 1f;

        [Header("Trigger Conditions (Để 0/Trống nếu bỏ qua)")]
        public int exactTurn = 0;
        public int minTurn = 0;   
        [Range(0f, 1f)] public float maxPlayerHpPercent = 1f; // VD: HP < 0.4
        [Range(0f, 1f)] public float maxEnemyHpPercent = 1f;  // VD: Enemy HP < 0.2
        public StatusData requiredStatus; // VD: Bắt buộc đang dính Procrastination
        public bool requireEliteCombat = false; // Chỉ xuất hiện đánh Elite

        [Header("Event Configuration")]
        public bool isAutoEvent = false; // Nếu True: Không hiện UI, tự động chạy Choice 0
        public List<EventChoice> choices = new();
    }
}