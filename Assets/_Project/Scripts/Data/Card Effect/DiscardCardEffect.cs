using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Discard_", menuName = "Student Life/Effects/Discard Card")]
    public class DiscardCardEffect : CardEffectData
    {
        public int discardAmount = 1;

        public override void Execute(ITargetable self, ITargetable target)
        {
            if (CombatManager.Instance != null && CombatManager.Instance.deckManager != null)
            {
                CombatManager.Instance.deckManager.DiscardRandomCardFromHand(discardAmount);
                Debug.Log($"[Effect] Đã bỏ đi {discardAmount} lá bài!");
            }
        }
    }
}