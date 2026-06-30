using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Visual;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Status_EnergyDebt", menuName = "Student Life/Status/Energy Debt")]
    public class EnergyDebtStatus : StatusData
    {
        public override void OnTurnStart(PlayerCharacter player, int stack)
        {
            if (CombatManager.Instance != null)
            {
                CombatManager.Instance.ConsumeEnergy(stack);

                NotificationManager.Instance.ShowMessage($"-{stack} Năng lượng do Cày Đêm!", Color.yellow);

                player.AddStatus(this, -stack);
            }
        }
    }
}