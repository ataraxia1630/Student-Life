using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Combat;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Status_Focused", menuName = "Student Life/Status/Focused")]
    public class FocusedStatus : StatusData
    {
        public override int ModifyDamage(PlayerCharacter player, CardInstance card, int currentDamage, int stack)
        {
            if (card.Data.type == CardType.Attack) return currentDamage + 3;
            return currentDamage;
        }

        public override void OnTurnEnd(PlayerCharacter player, int stack)
        {
            player.AddStatus(this, -1);
        }
    }
}