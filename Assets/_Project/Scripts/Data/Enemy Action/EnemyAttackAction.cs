using DoiSinhVien.Core;
using UnityEngine;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Enemy_Attack", menuName = "Student Life/Enemy Actions/Attack")]
    public class EnemyAttackAction : EnemyActionData
    {
        public override void Execute(ITargetable self, ITargetable target)
        {
            Debug.Log($"[Quái] Tung chiêu {actionName}!");
            target.TakeDamage(baseValue);
        }
    }
}
