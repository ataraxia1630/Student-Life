using DoiSinhVien.Core;
using UnityEngine;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Enemy_Defend", menuName = "Student Life/Enemy Actions/Defend")]
    public class EnemyDefendAction : EnemyActionData
    {
        public override void Execute(ITargetable self, ITargetable target)
        {
            Debug.Log($"[Quái] Tung chiêu {actionName}!");
            self.GainBlock(baseValue); 
        }
    }
}
