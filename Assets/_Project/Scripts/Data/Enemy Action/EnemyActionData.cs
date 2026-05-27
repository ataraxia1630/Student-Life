using DoiSinhVien.Core;
using UnityEngine;

namespace DoiSinhVien.Data
{
    public abstract class EnemyActionData : ScriptableObject
    {
        public string actionName;
        public IntentType intentType;

        public int minValue = 1;
        public int maxValue = 5;

        public int RollValue()
        {
            return Random.Range(minValue, maxValue + 1);
        }

        public abstract void Execute(ITargetable self, ITargetable target, int value);
    }
}
