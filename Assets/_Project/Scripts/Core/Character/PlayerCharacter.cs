using UnityEngine;

namespace DoiSinhVien.Core
{
    public class PlayerCharacter : MonoBehaviour, ITargetable
    {
        public int maxHealth = 100;
        private int currentHealth;
        public int CurrentHealth => currentHealth;

        private int currentBlock;
        public int CurrentBlock => currentBlock;

        public int bonusAttackDamage = 0;
        public int bonusDrawPerTurn = 0;
        public bool isNextCardDoubled = false;

        public void Initialize()
        {
            currentHealth = maxHealth;
            currentBlock = 0;
        }

        public void GainBlock(int amount)
        {
            currentBlock += amount;
            Debug.Log($"[Sinh Viên] Tạo {amount} Giáp phòng vệ.");
        }

        public void SetHealth(int amount)
        {
            currentHealth = amount;
        }

        public void TakeDamage(int amount)
        {
            if (currentBlock >= amount)
            {
                currentBlock -= amount;
                Debug.Log($"[Sinh Viên] Block đỡ hết! Mất {amount} Giáp, còn lại {currentBlock} Giáp.");
            }
            else
            {
                int remainingDamage = amount - currentBlock;
                currentBlock = 0;
                currentHealth -= remainingDamage;
                Debug.Log($"[Sinh Viên] Vỡ Block! Nhận {remainingDamage} sát thương. Tinh thần còn: {currentHealth}");

                if (currentHealth <= 0) Debug.Log("[Sinh Viên] BURNOUT! (Game Over)");
            }
        }

        public void ResetBlock()
        {
            currentBlock = 0;
        }
    }
}
