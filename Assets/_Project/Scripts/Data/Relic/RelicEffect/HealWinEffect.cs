using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "HealWinRelic", menuName = "Student Life/Relic Effects/Heal on Win")]
    public class HealWinEffect : RelicEffectData
    {
        public int healAmount = 5;

        public override void Execute(PlayerCharacter player, object context = null)
        {
            if (PlayerInventory.Instance != null)
            {
                PlayerInventory.Instance.currentHealth = Mathf.Min(
                    PlayerInventory.Instance.currentHealth + healAmount,
                    PlayerInventory.Instance.maxHealth
                );
                Debug.Log($"[Relic] Hồi {healAmount} Tinh thần.");
            }
        }
    }
}