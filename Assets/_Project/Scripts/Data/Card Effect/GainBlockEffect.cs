using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Block_", menuName = "Student Life/Effects/Gain Block")]
    public class GainBlockEffect : CardEffectData
    {
        public int blockAmount;

        public override void Execute(ITargetable self, ITargetable target)
        {
            self.GainBlock(blockAmount);
        }
    }
}