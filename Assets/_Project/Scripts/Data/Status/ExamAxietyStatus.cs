using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Combat;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Status_ExamAnxiety", menuName = "Student Life/Status/Exam Anxiety")]
    public class ExamAnxietyStatus : StatusData
    {
        public override int ModifyCost(PlayerCharacter player, CardInstance card, int currentCost, int stack)
        {
            float hpPercent = (float)player.CurrentHealth / player.MaxHealth;
            if (hpPercent < 0.4f) return currentCost + 1;

            return currentCost;
        }
    }
}