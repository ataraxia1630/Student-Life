using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Visual;
using DoiSinhVien.Combat;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "DealDamageEffect", menuName = "Student Life/Effects/Deal Damage")]
    public class DealDamageEffect : CardEffectData
    {
        public CardData compareCard;
        public override void Execute(ITargetable self, ITargetable target, CardInstance cardInstance)
        {
            if (cardInstance == null) return;

            int finalDamage = cardInstance.CurrentDamage;

            var discardPile = CombatManager.Instance.deckManager.discardPile;
            if (discardPile.Count > 0 && discardPile[discardPile.Count - 1].Data.id == compareCard.id)
            {
                finalDamage *= 2;
                Debug.Log($"[Effect] Copy-Paste Combo x2! Gây {finalDamage} DMG.");
            }
            else Debug.Log($"[Effect] Không có combo. Gây {finalDamage} DMG.");

            target.TakeDamage(finalDamage);

            if (target is MonoBehaviour targetObj)
            {
                GameEvents.OnPlayVFX?.Invoke(targetObj.transform, VFXType.Slash);
                Debug.Log($"[Effect] Thi triển {effectName}: Gây {finalDamage} sát thương!");
            }
        }
    }
}