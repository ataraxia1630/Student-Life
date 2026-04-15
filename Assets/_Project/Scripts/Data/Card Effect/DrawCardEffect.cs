using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Draw_", menuName = "Student Life/Effects/Draw Card")]
    public class DrawCardEffect : CardEffectData
    {
        public int drawAmount = 1;

        public override void Execute(ITargetable self, ITargetable target)
        {
            if (CombatManager.Instance != null && CombatManager.Instance.deckManager != null)
            {
                CombatManager.Instance.deckManager.DrawCard(drawAmount);
                Debug.Log($"[Effect] Đã rút thêm {drawAmount} lá bài!");
            }
        }
    }
}