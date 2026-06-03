using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Visual;

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

                if (target is MonoBehaviour targetObj)
                {
                    GameEvents.OnPlayVFX?.Invoke(targetObj.transform, VFXType.Slash);
                }
                Debug.Log($"[Effect] Thi triển {effectName}: Gây {finalDamage} sát thương!");
            }
            else Debug.Log($"[Effect] Thi triển {effectName}: Không có mục tiêu hợp lệ!");
        }
    }
}