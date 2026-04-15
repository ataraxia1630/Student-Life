using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    public abstract class CardEffectData : ScriptableObject
    {
        [Header("Effect Info")]
        public string effectName;

        public abstract void Execute(ITargetable self, ITargetable target);
    }
}