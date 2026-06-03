using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Combat; 

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Effect_ApplyStatus", menuName = "Student Life/Effects/Apply Status")]
    public class ApplyPlayerBuffEffect : CardEffectData
    {
        public StatusData statusToApply;

        public override void Execute(ITargetable self, ITargetable target, CardInstance cardInstance)
        {
            if (self is PlayerCharacter player && statusToApply != null && cardInstance != null)
            {
                int stackAmount = cardInstance.CurrentMagicNumber;

                if (stackAmount <= 0) stackAmount = 1;

                player.AddStatus(statusToApply, stackAmount);

                Debug.Log($"[Effect] Đã áp dụng {stackAmount} stack của {statusToApply.statusName} cho người chơi!");
            }
        }
    }
}