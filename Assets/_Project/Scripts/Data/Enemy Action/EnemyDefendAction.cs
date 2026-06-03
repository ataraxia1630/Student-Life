using DoiSinhVien.Core;
using DoiSinhVien.Visual;
using UnityEngine;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Enemy_Defend", menuName = "Student Life/Enemy Actions/Defend")]
    public class EnemyDefendAction : EnemyActionData
    {
        public override void Execute(ITargetable self, ITargetable target, int value)
        {
            if (self is MonoBehaviour targetObj)
            {
                GameEvents.OnPlayVFX?.Invoke(targetObj.transform, VFXType.Shield);
            }
            Debug.Log($"[Quái] Tung chiêu {actionName}!");
            self.GainBlock(value); 
        }
    }
}
