using System.Collections.Generic;
using UnityEngine;

namespace DoiSinhVien.Data
{
    [System.Serializable]
    public class EnemySlot
    {
        public List<EnemyTag> requiredTags;
        public List<EnemyTag> forbiddenTags;
    }

    [CreateAssetMenu(fileName = "NewBlueprint", menuName = "Student Life/Encounter Blueprint")]
    public class EncounterBlueprint : ScriptableObject
    {
        public string blueprintName;
        public int targetFloor; 

        public List<EnemySlot> slots;
    }
}

