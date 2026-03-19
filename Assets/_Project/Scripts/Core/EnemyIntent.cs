using UnityEngine;

namespace DoiSinhVien.Core
{
    public enum IntentType
    {
        Attack,
        Block,
        Buff,
        Debuff,
        Unknown
    }

    public class EnemyIntent
    {
        public IntentType Type;
        public int Value;

        public EnemyIntent(IntentType type, int value)
        {
            Type = type;
            Value = value;
        }
    }
}
