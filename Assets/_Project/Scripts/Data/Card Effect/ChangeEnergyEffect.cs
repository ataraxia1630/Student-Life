using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Energy_", menuName = "Student Life/Effects/Change Energy")]
    public class ChangeEnergyEffect : CardEffectData
    {
        public int energyAmount;

        public override void Execute(ITargetable self, ITargetable target)
        {
            if (CombatManager.Instance != null)
            {
                CombatManager.Instance.currentEnergy = Mathf.Clamp(
                    CombatManager.Instance.currentEnergy + energyAmount,
                    0,
                    CombatManager.Instance.maxEnergy
                );
                Debug.Log($"[Effect] Năng lượng thay đổi {energyAmount}. Hiện tại: {CombatManager.Instance.currentEnergy}");
            }
        }
    }
}