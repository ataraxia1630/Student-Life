using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "EnergyStartRelic", menuName = "Student Life/Relic Effects/Energy Start")]
    public class EnergyStartEffect : RelicEffectData
    {
        public int bonusEnergy = 1;

        public override void Execute(PlayerCharacter player, object context = null)
        {
            if (CombatManager.Instance != null)
            {
                CombatManager.Instance.currentEnergy += bonusEnergy;
                Debug.Log($"[Relic] +{bonusEnergy} Energy.");
            }
        }
    }
}