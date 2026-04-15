using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "ComboB_", menuName = "Student Life/Effects/Combo Block")]
    public class ComboBlockEffect : CardEffectData
    {
        public int baseBlock = 5;
        public int comboBonusBlock = 5;

        public override void Execute(ITargetable self, ITargetable target)
        {
            int finalBlock = baseBlock;
            var discardPile = CombatManager.Instance.deckManager.discardPile;

            if (discardPile.Count > 0 && discardPile[discardPile.Count - 1].Data.type == CardType.Defend)
            {
                finalBlock += comboBonusBlock;
                Debug.Log($"[Effect] VPN Trường kích hoạt Combo Defend! Nhận thêm {comboBonusBlock} Giáp.");
            }

            self.GainBlock(finalBlock);
        }
    }
}