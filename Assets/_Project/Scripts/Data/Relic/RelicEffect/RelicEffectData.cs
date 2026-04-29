using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    public enum RelicTrigger
    {
        CombatStart,
        TurnStart,
        TurnEnd,
        CardPlayed,
        CombatWin
    }

    public abstract class RelicEffectData : ScriptableObject
    {
        public RelicTrigger triggerMoment;

        public abstract void Execute(PlayerCharacter player, object context = null);
    }
}