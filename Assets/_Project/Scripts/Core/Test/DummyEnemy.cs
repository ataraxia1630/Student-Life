using UnityEngine;

namespace DoiSinhVien.Core
{
    public class DummyEnemy : MonoBehaviour, ITargetable
    {
        [Header("Stats")]
        public string enemyName = "Bug Bự";
        public int currentHealth = 50;

        public int CurrentHealth => currentHealth;

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            Debug.Log($"[Quái - {enemyName}] Bị trừ {amount} máu! Máu còn lại: {currentHealth}");

            if (currentHealth <= 0)
            {
                Debug.Log($"[Quái - {enemyName}] Đã bị fix (Bay màu)!");
            }
        }

        public void GainBlock(int amount)
        {
            Debug.Log($"[Quái - {enemyName}] Nhận {amount} Giáp (Chưa implement logic giáp cho test này).");
        }

        public void SetHealth(int amount)
        {
            currentHealth = amount;
            Debug.Log($"[Hệ thống] Máu của {enemyName} được reset về: {currentHealth}");
        }
    }
}