using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "DealDamageEffect", menuName = "Student Life/Effects/Deal Damage")]
    public class DealDamageEffect : CardEffectData
    {
        [Header("Damage Settings")]
        public int damageAmount;

        public override void Execute(ITargetable self, ITargetable target)
        {
            if (target != null)
            {
                int finalDamage = damageAmount;
                if (self is PlayerCharacter player)
                {
                    finalDamage += player.bonusAttackDamage;
                }
                target.TakeDamage(finalDamage);
                Debug.Log($"[Effect] Thi triển {effectName}: Gây {finalDamage} sát thương!");
            }
            else Debug.Log($"[Effect] Thi triển {effectName}: Không có mục tiêu hợp lệ!");
        }
    }
}