using UnityEngine;
using DoiSinhVien.Combat;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Status_Imposter", menuName = "Student Life/Status/Imposter Syndrome")]
    public class ImposterSyndromeStatus : StatusData
    {
        [SerializeField] private int modifier = -1;
        public override int ModifyCost(CardInstance card, int currentCost, int stack)
        {
            return currentCost + modifier;
        }
    }
}