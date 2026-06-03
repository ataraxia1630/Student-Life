using UnityEngine;
using DoiSinhVien.Combat;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Status_Strength", menuName = "Student Life/Status/Strength")]
    public class StrengthStatus : StatusData
    {
        public override int ModifyDamage(PlayerCharacter player, CardInstance card, int currentDamage, int stack)
        {
            if (card.Data.type == CardType.Attack)
                return currentDamage + stack; 

            return currentDamage;
        }
    }
}