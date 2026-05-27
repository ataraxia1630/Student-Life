using DoiSinhVien.Core;
using UnityEngine;
using System.Collections.Generic;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Enemy_BuffAlly", menuName = "Student Life/Enemy Actions/Buff Ally Block")]
    public class EnemyBuffAllyAction : EnemyActionData
    {
        public override void Execute(ITargetable self, ITargetable target, int value)
        {
            if (CombatManager.Instance == null) return;

            List<EnemyController> allies = CombatManager.Instance.activeEnemies.FindAll(e => e.CurrentHealth > 0 && (ITargetable)e != self);

            if (allies.Count > 0)
            {
                EnemyController allyToBuff = allies[Random.Range(0, allies.Count)];
                Debug.Log($"[Quái] Tung chiêu {actionName}! Buff {value} Giáp cho {allyToBuff.data.enemyName}");
                allyToBuff.GainBlock(value);
            }
            else
            {
                Debug.Log($"[Quái] Định dùng {actionName} nhưng đồng bọn chết hết rồi!");
            }
        }
    }
}