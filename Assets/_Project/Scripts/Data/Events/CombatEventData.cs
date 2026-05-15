using System.Collections.Generic;
using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    public enum EventTriggerTiming { OnTurnStart, OnTurnEnd }

    [System.Serializable]
    public class EventChoice
    {
        [Tooltip("Dòng chữ hiện trên nút bấm")]
        public string choiceText;

        [Tooltip("Các hiệu ứng sẽ xảy ra khi bấm nút này")]
        public List<CardEffectData> consequences;
    }

    [CreateAssetMenu(fileName = "NewCombatEvent", menuName = "Student Life/Combat Event Data")]
    public class CombatEventData : ScriptableObject
    {
        public string eventName;
        [TextArea] public string description;

        [Header("Trigger Settings")]
        public EventTriggerTiming triggerTiming;
        [Range(0f, 1f)] public float triggerChance = 0.1f;

        [Header("Player Choices (Tối đa 3)")]
        public List<EventChoice> choices = new();
    }
}