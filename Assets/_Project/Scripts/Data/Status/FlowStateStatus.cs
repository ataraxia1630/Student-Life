using DoiSinhVien.Core;
using DoiSinhVien.Visual;
using UnityEngine;

namespace DoiSinhVien.Data
{
    public class FlowStateStatus: StatusData
    {
        public override void OnTurnStart(PlayerCharacter player, int stack)
        {
            player.TakeDamage(stack);
            NotificationManager.Instance.ShowMessage($"Trúng độc: -{stack} HP!", Color.purple);

            player.AddStatus(this, -1);
        }
    }
}
