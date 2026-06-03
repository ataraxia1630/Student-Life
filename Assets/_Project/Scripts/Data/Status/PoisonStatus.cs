using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Visual;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Status_Poison", menuName = "Student Life/Status/Poison")]
    public class PoisonStatus : StatusData
    {
        public override void OnTurnStart(PlayerCharacter player, int stack)
        {
            player.TakeDamage(stack);
            NotificationManager.Instance.ShowMessage($"Trúng độc: -{stack} HP!", Color.purple);

            player.AddStatus(this, -1);
        }
    }
}