using UnityEngine;
using DoiSinhVien.Data;
using DoiSinhVien.Combat;

namespace DoiSinhVien.Core
{
    public class RelicManager : MonoBehaviour
    {
        private void OnEnable()
        {
            GameEvents.OnCombatStart += HandleCombatStart;
            GameEvents.OnCombatWin += HandleCombatWin;
            GameEvents.OnCardPlayed += HandleCardPlayed;
        }

        private void OnDisable()
        {
            GameEvents.OnCombatStart -= HandleCombatStart;
            GameEvents.OnCombatWin -= HandleCombatWin;
            GameEvents.OnCardPlayed -= HandleCardPlayed;
        }

        private void TriggerRelics(RelicTrigger trigger, object context = null)
        {
            if (PlayerInventory.Instance == null || PlayerInventory.Instance.ownedRelics.Count == 0) return;

            PlayerCharacter player = null;
            if (CombatManager.Instance != null) player = CombatManager.Instance.player;

            foreach (var relic in PlayerInventory.Instance.ownedRelics)
            {
                if (relic.passiveEffects == null) continue;

                foreach (var effect in relic.passiveEffects)
                {
                    if (effect.triggerMoment == trigger)
                    {
                        effect.Execute(player, context);
                    }
                }
            }
        }

        private void HandleCombatStart() => TriggerRelics(RelicTrigger.CombatStart);
        private void HandleCombatWin() => TriggerRelics(RelicTrigger.CombatWin);
        private void HandleCardPlayed(CardInstance card) => TriggerRelics(RelicTrigger.CardPlayed, card);
    }
}