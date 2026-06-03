using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Visual;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Block_", menuName = "Student Life/Effects/Gain Block")]
    public class GainBlockEffect : CardEffectData
    {
        public int blockAmount;

        public override void Execute(ITargetable self, ITargetable target)
        {
            if (self is MonoBehaviour targetObj)
            {
                GameEvents.OnPlayVFX?.Invoke(targetObj.transform, VFXType.Shield);
            }
            self.GainBlock(blockAmount);
        }
    }
}