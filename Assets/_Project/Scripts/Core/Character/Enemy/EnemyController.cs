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
        public int CurrentStrength { get; private set; }

        public EnemyActionData CurrentAction { get; private set; }
        public int CurrentIntentValue { get; private set; }

        public bool isStunned; 

        public void Initialize()
        {
            CurrentHealth = data.maxHealth;
            CurrentBlock = 0;
            CurrentStrength = 0;
            Debug.Log($"[Spawn] {data.enemyName} xuất hiện với {CurrentHealth} HP.");
        }

        public void DetermineNextIntent()
        {
            if (data.actionPool == null || data.actionPool.Count == 0) return;

            int randomIndex = Random.Range(0, data.actionPool.Count);
            CurrentAction = data.actionPool[randomIndex];

            CurrentIntentValue = CurrentAction.RollValue();
            int displayValue = CurrentIntentValue;
            if (CurrentAction.intentType == IntentType.Attack) displayValue += CurrentStrength;

            Debug.Log($"[Intent] {data.enemyName} chuẩn bị dùng {CurrentAction.actionName} ({CurrentAction.intentType} {displayValue})");
        }

        public void ExecuteIntent(ITargetable playerTarget)
        {
            if (CurrentAction != null)
            {
                if (isStunned)
                {
                    Debug.Log($"[Quái - {data.enemyName}] Bị stun, bỏ qua lượt này!");
                    isStunned = false;
                }
                else
                    CurrentAction.Execute(this, playerTarget, CurrentIntentValue);
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

                GameEvents.OnEntityDamaged?.Invoke(this.transform, remainingDamage);
                Debug.Log($"[Quái - {data.enemyName}] Vỡ Block! Nhận {remainingDamage} sát thương. Máu còn: {CurrentHealth}");

                if (CurrentHealth <= 0)
                {
                    Debug.Log($"[Quái - {data.enemyName}] Đã bị fix (Bay màu)!");
                    Die();
                    // Gọi event quái chết để cập nhật list activeEnemies
                }
            }
        }

        private void Die()
        {
            CurrentHealth = 0;
            CurrentBlock = 0;
            //ActiveStatuses?.Clear();

            Debug.Log($"[Quái - {data.enemyName}] Đã biến thành MỘ!");

            Collider2D col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
            if (sprite != null)
            {
                sprite.color = Color.gray;
            }

            Canvas uiCanvas = GetComponentInChildren<Canvas>();
            if (uiCanvas != null) uiCanvas.gameObject.SetActive(false);

            // Kêu UI vẽ lại Icon Status (để xóa hết icon buff cũ)
            //OnStatusChanged?.Invoke();
        }

        public void GainBlock(int amount)
        {
            CurrentBlock += amount;

            GameEvents.OnEntityGainedBlock?.Invoke(this.transform, amount);
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

        public void GainStrength(int amount)
        {
            CurrentStrength += amount;
            Debug.Log($"[Quái - {data.enemyName}] Nổi điên! Tăng {amount} Sức mạnh. Sức mạnh hiện tại: {CurrentStrength}");
        }
    }
}