using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "ApplyStatus", menuName = "Student Life/Enemy Actions/Apply Status")]
    public class EnemyApplyStatusAction : EnemyActionData 
    {
        [Tooltip("Kéo thả Hội chứng hoặc Debuff (VD: Phone Addiction) vào đây")]
        public StatusData statusToApply;

        public override void Execute(ITargetable self, ITargetable target, int value)
        {
            if (target is PlayerCharacter player && statusToApply != null)
            {
                player.AddStatus(statusToApply, value);

                Debug.Log($"[Quái] Đã yếm {value} stack {statusToApply.statusName} lên Player!");

                //if (target is MonoBehaviour targetObj)
                //{
                //    GameEvents.OnPlayVFX?.Invoke(targetObj.transform, VFXType.Buff);
                //}
            }
        }
    }
}