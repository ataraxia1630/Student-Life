using DoiSinhVien.Core;
using UnityEngine;

namespace DoiSinhVien.Data
{
    public abstract class EnemyActionData : ScriptableObject
    {
        public string actionName;
        public IntentType intentType;
        public int baseValue; 

        public abstract void Execute(ITargetable self, ITargetable target);
    }
}
