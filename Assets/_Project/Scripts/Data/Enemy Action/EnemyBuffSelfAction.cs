using DoiSinhVien.Core;
using UnityEngine;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Enemy_BuffSelf", menuName = "Student Life/Enemy Actions/Buff Self Strength")]
    public class EnemyBuffSelfAction : EnemyActionData
    {
        public override void Execute(ITargetable self, ITargetable target, int value)
        {
            Debug.Log($"[Quái] Tung chiêu {actionName} buff {value} Sức mạnh!");

            if (self is EnemyController enemy)
            {
                enemy.GainStrength(value);
            }
        }
    }
}