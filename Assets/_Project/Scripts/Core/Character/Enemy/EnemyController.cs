using UnityEngine;
using DoiSinhVien.Data;

namespace DoiSinhVien.Core
{
    public class EnemyController : MonoBehaviour, ITargetable
    {
        [Header("Data Source")]
        public EnemyData data; 

        public int CurrentHealth { get; private set; }
        public int CurrentBlock { get; private set; }

        public EnemyActionData CurrentAction { get; private set; }

        public void Initialize()
        {
            CurrentHealth = data.maxHealth;
            CurrentBlock = 0;
            Debug.Log($"[Spawn] {data.enemyName} xuất hiện với {CurrentHealth} HP.");
        }

        public void DetermineNextIntent()
        {
            if (data.actionPool == null || data.actionPool.Count == 0) return;

            int randomIndex = Random.Range(0, data.actionPool.Count);
            CurrentAction = data.actionPool[randomIndex];

            Debug.Log($"[Intent] {data.enemyName} chuẩn bị dùng {CurrentAction.actionName} ({CurrentAction.intentType} {CurrentAction.baseValue})");
        }

        public void ExecuteIntent(ITargetable playerTarget)
        {
            if (CurrentAction != null)
            {
                CurrentAction.Execute(this, playerTarget);
                CurrentAction = null; 
            }
        }

        public void TakeDamage(int amount)
        {
            if (CurrentBlock >= amount)
            {
                CurrentBlock -= amount;
                Debug.Log($"[Quái - {data.enemyName}] Block đỡ hết! Mất {amount} Giáp, còn {CurrentBlock} Giáp.");
            }
            else
            {
                int remainingDamage = amount - CurrentBlock;
                CurrentBlock = 0;
                CurrentHealth -= remainingDamage;
                Debug.Log($"[Quái - {data.enemyName}] Vỡ Block! Nhận {remainingDamage} sát thương. Máu còn: {CurrentHealth}");

                if (CurrentHealth <= 0)
                {
                    Debug.Log($"[Quái - {data.enemyName}] Đã bị fix (Bay màu)!");
                }
            }
        }

        public void GainBlock(int amount)
        {
            CurrentBlock += amount;
            Debug.Log($"[Quái - {data.enemyName}] Nhận {amount} Giáp.");
        }

        public void SetHealth(int amount)
        {
            CurrentHealth = amount;
            Debug.Log($"[Hệ thống] Máu của {data.enemyName} được reset về: {CurrentHealth}");
        }

        public void ResetBlock()
        {
            CurrentBlock = 0;
            Debug.Log($"[Hệ thống] Giáp của {data.enemyName} được reset về 0");
        }
    }
}