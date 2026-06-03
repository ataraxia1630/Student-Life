using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Visual;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "AgileMindsetStatus", menuName = "Student Life/Status/Agile Mindset")]
    public class AgileMindsetStatus : StatusData
    {
        public override void OnTurnStart(PlayerCharacter player, int stack)
        {
            if (CombatManager.Instance != null && CombatManager.Instance.deckManager != null)
            {
                CombatManager.Instance.deckManager.DrawCard(amount: stack);

                NotificationManager.Instance.ShowMessage("Agile Mindset: Rút thêm bài!", Color.cyan);
            }
        }
    }
}