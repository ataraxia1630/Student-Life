using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Combat;
using DoiSinhVien.Visual;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Effect_BuffHandAttack", menuName = "Student Life/Effects/Buff Hand Attack")]
    public class BuffHandAttackEffect : CardEffectData
    {
        public override void Execute(ITargetable self, ITargetable target, CardInstance cardInstance)
        {
            if (CombatManager.Instance == null || CombatManager.Instance.deckManager == null || cardInstance == null) return;

            int buffAmount = cardInstance.CurrentMagicNumber;
            int buffCount = 0;

            foreach (var card in CombatManager.Instance.deckManager.hand)
            {
                if (card.Data.type == CardType.Attack)
                {
                    card.bonusDamage += buffAmount;

                    card.RecalculateStats(CombatManager.Instance.player);
                    buffCount++;
                }
            }

            if (buffCount > 0)
            {
                NotificationManager.Instance.ShowMessage($"Đã buff +{buffAmount} DMG cho các lá bài attack trên tay!", Color.green);
            }
        }
    }
}