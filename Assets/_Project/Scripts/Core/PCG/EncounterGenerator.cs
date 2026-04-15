using DoiSinhVien.Data;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

namespace DoiSinhVien.Core
{
    public class EncounterGenerator : MonoBehaviour
    {
        [Header("Database")]
        public List<EnemyData> entireEnemyPool; 

        public List<EnemyData> GenerateEncounter(EncounterBlueprint blueprint)
        {
            List<EnemyData> finalEncounter = new();

            foreach (EnemySlot slot in blueprint.slots)
            {
                List<EnemyData> validEnemies = entireEnemyPool.Where(enemy =>
                    slot.requiredTags.All(reqTag => enemy.HasTag(reqTag)) &&
                    !slot.forbiddenTags.Any(forbTag => enemy.HasTag(forbTag))
                ).ToList();

                if (validEnemies.Count > 0)
                {
                    int randomIndex = Random.Range(0, validEnemies.Count);
                    finalEncounter.Add(validEnemies[randomIndex]);
                }
                else
                {
                    Debug.LogWarning($"Không tìm thấy quái phù hợp cho slot trong Blueprint: {blueprint.blueprintName}");
                }
            }

            return finalEncounter;
        }
    }
}

