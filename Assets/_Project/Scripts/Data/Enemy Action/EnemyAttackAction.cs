using DoiSinhVien.Core;
using DoiSinhVien.Visual;
using UnityEngine;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Enemy_Attack", menuName = "Student Life/Enemy Actions/Attack")]
    public class EnemyAttackAction : EnemyActionData
    {
        public override void Execute(ITargetable self, ITargetable target, int value)
        {
            int finalDamage = value;

            if (self is EnemyController enemy)
            {
                finalDamage += enemy.CurrentStrength;
            }

            if (target is MonoBehaviour targetObj)
            {
                GameEvents.OnPlayVFX?.Invoke(targetObj.transform, VFXType.Slash);
            }
            Debug.Log($"[Quái] Tung chiêu {actionName}!");
            target.TakeDamage(finalDamage);
        }
    }
}
