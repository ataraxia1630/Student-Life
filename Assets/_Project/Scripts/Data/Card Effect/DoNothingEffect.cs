using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Effect_DoNothing", menuName = "Student Life/Effects/Do Nothing")]
    public class DoNothingEffect : CardEffectData
    {
        public override void Execute(ITargetable self, ITargetable target)
        {
            Debug.Log("[Effect] Lá bài rác đã bị tiêu thụ!");
        }
    }
}