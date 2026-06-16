using DoiSinhVien.Core;
using DoiSinhVien.Data;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Student Life/Event Outcomes/Skip Turn Outcome")]
public class SkipTurnOutcome : EventOutcomeData
{
    public override void Execute(CombatManager combatManager)
    {
        combatManager.SkipNextTurn();
    }
}
