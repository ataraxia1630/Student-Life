using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Combat;
using DoiSinhVien.Visual;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "ImposterSyndromeStatus", menuName = "Student Life/Status/Imposter Syndrome")]
    public class ImposterSyndromeStatus : StatusData
    {
        public override int ModifyCost(PlayerCharacter player, CardInstance card, int currentCost, int stack)
        {
            return currentCost - 1; 
        }

        public override void OnTurnStart(PlayerCharacter player, int stack)
        {
            player.TakeDamage(2); 
            NotificationManager.Instance.ShowMessage("Imposter: Mất 2 Tinh thần!", Color.magenta);
        }
    }
}