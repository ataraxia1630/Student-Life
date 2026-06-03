using UnityEngine;
using DoiSinhVien.Combat;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Status_Weak", menuName = "Student Life/Status/Weak")]
    public class WeakStatus : StatusData
    {
        [SerializeField] private float damageReductionPercent = 0.25f;
        public override int ModifyDamage(CardInstance card, int currentDamage, int stack)
        {
            if (card.Data.type == CardType.Attack)
                return Mathf.FloorToInt(currentDamage * (1 - damageReductionPercent));

            return currentDamage;
        }
    }
}