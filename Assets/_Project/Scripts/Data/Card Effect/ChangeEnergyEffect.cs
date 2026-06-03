using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Combat;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Energy_", menuName = "Student Life/Effects/Change Energy")]
    public class ChangeEnergyEffect : CardEffectData
    {
        public int energyAmount;

        public override void Execute(ITargetable self, ITargetable target, CardInstance cardInstance)
        {
            if (CombatManager.Instance != null)
            {
                CombatManager.Instance.currentEnergy += energyAmount;
                Debug.Log($"[Effect] Năng lượng thay đổi {energyAmount}. Hiện tại: {CombatManager.Instance.currentEnergy}");
            }
        }
    }
}