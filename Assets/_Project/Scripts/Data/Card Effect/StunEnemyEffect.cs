using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Combat;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "StunEnemy", menuName = "Student Life/Effects/Stun Enemy")]
    public class StunEnemyEffect : CardEffectData
    {
        public override void Execute(ITargetable self, ITargetable target, CardInstance cardInstance)
        {
            if (target is EnemyController enemy)
            {
                enemy.isStunned = true; 
                Debug.Log($"[Effect] {enemy.data.enemyName} đã bị Xin Deadline thành công! Bỏ qua 1 lượt hành động.");
            }
        }
    }
}