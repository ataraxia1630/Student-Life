using DoiSinhVien.Core;
using DoiSinhVien.Data;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Student Life/Event Outcomes/Chance Outcome")]
public class ChanceOutcome : EventOutcomeData
{
    [Range(0f, 1f)] public float successChance = 0.5f;
    public List<EventOutcomeData> successOutcomes;
    public List<EventOutcomeData> failOutcomes;

    public override void Execute(CombatManager combatManager)
    {
        float roll = Random.value;
        var outcomesToRun = roll <= successChance ? successOutcomes : failOutcomes;

        foreach (var outcome in outcomesToRun)
        {
            outcome.Execute(combatManager);
        }
    }
}