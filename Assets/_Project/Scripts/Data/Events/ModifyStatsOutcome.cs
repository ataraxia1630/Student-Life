using DoiSinhVien.Core;
using DoiSinhVien.Data;
using UnityEngine;

[CreateAssetMenu(menuName = "Student Life/Event Outcomes/Modify Stats")]
public class ModifyStatsOutcome : EventOutcomeData
{
    public int hpChange;
    public int energyChange;
    public int blockChange;

    public override void Execute(CombatManager combatManager)
    {
        if (hpChange > 0) combatManager.player.Heal(hpChange);
        else if (hpChange < 0) combatManager.player.TakeDamage(-hpChange);

        if (energyChange != 0) combatManager.currentEnergy += energyChange; 

        if (blockChange > 0) combatManager.player.GainBlock(blockChange);
    }
}