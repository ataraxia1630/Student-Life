using UnityEngine;
using DoiSinhVien.Data;
using UnityEngine.InputSystem;

namespace DoiSinhVien.Core
{
    public class CombatTester : MonoBehaviour
    {
        [Header("Setup Test")]
        public CardData cardToPlay; 
        public DummyEnemy targetEnemy; 

        public void PlayCard()
        {
            if (cardToPlay == null || targetEnemy == null)
            {
                Debug.LogError("Thiếu Data! Hãy kéo thả CardData và DummyEnemy vào CombatTester.");
                return;
            }

            Debug.Log($"\n--- ĐÁNH BÀI: {cardToPlay.cardName} (Tốn {cardToPlay.manaCost} Energy) ---");
            Debug.Log($"[Flavor]: {cardToPlay.flavorText}");

            foreach (var effect in cardToPlay.effects)
            {
                if (effect != null)
                {
                    effect.Execute(targetEnemy);
                }
            }
        }

        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                PlayCard();
            }
        }
    }
}