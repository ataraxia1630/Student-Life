using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    public abstract class EventOutcomeData : ScriptableObject
    {
        public abstract void Execute(CombatManager combatManager);
    }
}