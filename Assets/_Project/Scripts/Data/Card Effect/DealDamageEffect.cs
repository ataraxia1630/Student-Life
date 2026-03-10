using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "DealDamageEffect", menuName = "Student Life/Effects/Deal Damage")]
    public class DealDamageEffect : CardEffectData
    {
        [Header("Damage Settings")]
        public int damageAmount;

        public override void Execute(ITargetable target)
        {
            if (target != null)
            {
                target.TakeDamage(damageAmount);
                Debug.Log($"[Effect] Thi triển {effectName}: Gây {damageAmount} sát thương!");
            }
            else Debug.Log($"[Effect] Thi triển {effectName}: Không có mục tiêu hợp lệ!");
        }
    }
}