using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Combat;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Effect_DoNothing", menuName = "Student Life/Effects/Do Nothing")]
    public class DoNothingEffect : CardEffectData
    {
        public override void Execute(ITargetable self, ITargetable target, CardInstance cardInstance)
        {
            Debug.Log("[Effect] Lá bài rác đã bị tiêu thụ!");
        }
    }
}