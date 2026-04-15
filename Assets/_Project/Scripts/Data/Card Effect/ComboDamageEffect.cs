using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "ComboD_", menuName = "Student Life/Effects/Combo Damage")]
    public class ComboDamageEffect : CardEffectData
    {
        public int baseDamage = 3;
        public string requiredPreviousCardId; 

        public override void Execute(ITargetable self, ITargetable target)
        {
            int finalDamage = baseDamage;
            var discardPile = CombatManager.Instance.deckManager.discardPile;

            if (discardPile.Count > 0 && discardPile[discardPile.Count - 1].Data.id == requiredPreviousCardId)
            {
                finalDamage *= 2; 
                Debug.Log($"[Effect] Copy-Paste Combo x2! Gây {finalDamage} DMG.");
            }
            else
            {
                Debug.Log($"[Effect] Không có combo. Gây {finalDamage} DMG.");
            }

            target.TakeDamage(finalDamage);
        }
    }
}