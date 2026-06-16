using DoiSinhVien.Core;
using DoiSinhVien.Data;
using UnityEngine;

[CreateAssetMenu(menuName = "Student Life/Event Outcomes/Status Modify")]
public class StatusOutcome : EventOutcomeData
{
    public StatusData status;
    public int amount; // Số âm là xóa, số dương là thêm

    public override void Execute(CombatManager combatManager)
    {
        combatManager.player.AddStatus(status, amount);
    }
}