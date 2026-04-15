using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "RiskDamage", menuName = "Student Life/Effects/Damage with Risk")]
    public class RiskDamageEffect : CardEffectData
    {
        public int damageToEnemy;
        [Range(0f, 1f)] public float riskChance = 0.3f; 
        public int selfDamage;

        public override void Execute(ITargetable self, ITargetable target)
        {
            target.TakeDamage(damageToEnemy);
            Debug.Log($"[Effect] Gây {damageToEnemy} DMG lên quái.");

            if (Random.value <= riskChance)
            {
                self.TakeDamage(selfDamage);
                Debug.Log($"[Effect] Oops! Deploy lỗi, tự nhận {selfDamage} DMG.");
            }
        }
    }
}