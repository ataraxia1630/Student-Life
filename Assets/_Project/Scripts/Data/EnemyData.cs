using UnityEngine;
using System.Collections.Generic;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "Student Life/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public string enemyId;
        public string enemyName;
        public int maxHealth;

        public List<EnemyActionData> actionPool;
    }
}