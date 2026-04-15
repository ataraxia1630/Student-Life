using DoiSinhVien.Core;
using DoiSinhVien.Data;
using System.Collections.Generic;
using UnityEngine;

namespace DoiSinhVien.Core
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }

        [Header("References")]
        public EncounterGenerator generator; 

        [Header("Battle Setup")]
        [Tooltip("Các vị trí (Transform) trống trên Scene để đặt quái vật")]
        public List<Transform> enemySpawnPoints;

        private List<GameObject> activeEnemiesOnScene = new();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            if (RunManager.Instance != null && RunManager.Instance.pendingEncounter != null)
            {
                Debug.Log($"Đã nhận lệnh từ Map! Đang sinh quái cho Blueprint: {RunManager.Instance.pendingEncounter.blueprintName}");

                StartBattle(RunManager.Instance.pendingEncounter);

                RunManager.Instance.pendingEncounter = null;
            }
            else
            {
                Debug.LogWarning("Test Mode: Không có dữ liệu từ Map. Đang tự động test với dữ liệu mặc định (nếu có).");
            }
        }

        public void StartBattle(EncounterBlueprint blueprint)
        {
            ClearCurrentEnemies();

            List<EnemyData> spawnedEnemyData = generator.GenerateEncounter(blueprint);
            List<EnemyController> controllersForCombat = new();

            for (int i = 0; i < spawnedEnemyData.Count; i++)
            {
                if (i >= enemySpawnPoints.Count)
                {
                    Debug.LogWarning("Không đủ vị trí Spawn cho toàn bộ quái!");
                    break;
                }

                GameObject newEnemyObj = Instantiate(
                    spawnedEnemyData[i].enemyPrefab,
                    enemySpawnPoints[i].position,
                    Quaternion.identity
                );

                newEnemyObj.name = spawnedEnemyData[i].enemyName + $"_{i}";

                activeEnemiesOnScene.Add(newEnemyObj);

                EnemyController controller = newEnemyObj.GetComponent<EnemyController>();
                if (controller != null)
                {
                    controller.data = spawnedEnemyData[i];
                    controller.Initialize();
                    controllersForCombat.Add(controller);
                }
                else
                {
                    Debug.LogWarning($"Prefab {newEnemyObj.name} thiếu component EnemyController!");
                }
            }

            Debug.Log($"Đã spawn xong {spawnedEnemyData.Count} quái nghênh chiến!");

            if (CombatManager.Instance != null && controllersForCombat.Count > 0)
            {
                CombatManager.Instance.StartCombat(controllersForCombat);
            }
        }

        private void ClearCurrentEnemies()
        {
            foreach (GameObject enemy in activeEnemiesOnScene)
            {
                Destroy(enemy);
            }
            activeEnemiesOnScene.Clear();
        }
    }
}