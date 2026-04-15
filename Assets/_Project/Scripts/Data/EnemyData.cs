using UnityEngine;
using System.Collections.Generic;

namespace DoiSinhVien.Data
{
    public enum EnemyTag
    {
        Frontline, Support, Minion, HighBurstDamage, Fire, Ice, Tier1, Tier2
    }


    [CreateAssetMenu(fileName = "NewEnemy", menuName = "Student Life/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public string enemyId;
        public string enemyName;
        public GameObject enemyPrefab;
        public int maxHealth;

        public List<EnemyActionData> actionPool;

        public List<EnemyTag> tags;
        public bool HasTag(EnemyTag tag)
        {
            return tags.Contains(tag);
        }
    }
}