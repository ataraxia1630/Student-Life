using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Visual;
using DoiSinhVien.Combat;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Block_", menuName = "Student Life/Effects/Gain Block")]
    public class GainBlockEffect : CardEffectData
    {
        public int comboBonusBlock = 5;
        public CardData compareCard;

        public override void Execute(ITargetable self, ITargetable target, CardInstance cardInstance)
        {
            if (cardInstance == null) return;

            int finalBlock = cardInstance.CurrentBlock;

            var discardPile = CombatManager.Instance.deckManager.discardPile;
            if (discardPile.Count > 0 && discardPile[discardPile.Count - 1].Data.id == compareCard.id)
            {
                finalBlock += comboBonusBlock;
                Debug.Log($"[Effect] VPN Trường kích hoạt Combo Defend! Nhận thêm {comboBonusBlock} Giáp.");
            }
            else Debug.Log($"[Effect] Không có combo. Nhận {finalBlock} Giáp.");

            self.GainBlock(finalBlock);

            if (self is MonoBehaviour targetObj)
            {
                GameEvents.OnPlayVFX?.Invoke(targetObj.transform, VFXType.Shield);
            }
        }
    }
}