using DoiSinhVien.Core;
using UnityEngine;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Enemy_Clog", menuName = "Student Life/Enemy Actions/Clog Deck")]
    public class EnemyClogAction : EnemyActionData
    {
        [Tooltip("Thẻ bài rác sẽ nhét vào người chơi (vd: Thẻ Stress)")]
        public CardData trashCardData;

        public override void Execute(ITargetable self, ITargetable target, int value)
        {
            Debug.Log($"[Quái] Tung chiêu {actionName}! Nhét {value} lá {trashCardData.cardName} vào bộ bài!");

            if (CombatManager.Instance != null && CombatManager.Instance.deckManager != null)
            {
                for (int i = 0; i < value; i++)
                {
                    CombatManager.Instance.deckManager.discardPile.Add(new Combat.CardInstance(trashCardData));
                }
            }
        }
    }
}