using UnityEngine;

namespace DoiSinhVien.Core
{
    public class PlayerCharacter : MonoBehaviour, ITargetable
    {
        public int maxHealth = 100;

        public int CurrentHealth { get; private set; }
        public int CurrentBlock { get; private set; }

        public int bonusAttackDamage = 0;
        public int bonusDrawPerTurn = 0;
        public bool isNextCardDoubled = false;

        public void Initialize()
        {
            CurrentHealth = maxHealth;
            CurrentBlock = 0;
        }

        public void GainBlock(int amount)
        {
            CurrentBlock += amount;
            Debug.Log($"[Sinh Viên] Tạo {amount} Giáp phòng vệ.");
        }

        public void SetHealth(int amount)
        {
            CurrentHealth = amount;
        }

        public void TakeDamage(int amount)
        {
            if (CurrentBlock >= amount)
            {
                CurrentBlock -= amount;
                Debug.Log($"[Sinh Viên] Block đỡ hết! Mất {amount} Giáp, còn lại {CurrentBlock} Giáp.");
            }
            else
            {
                int remainingDamage = amount - CurrentBlock;
                CurrentBlock = 0;
                CurrentHealth -= remainingDamage;
                Debug.Log($"[Sinh Viên] Vỡ Block! Nhận {remainingDamage} sát thương. Tinh thần còn: {CurrentHealth}");

                if (CurrentHealth <= 0) Debug.Log("[Sinh Viên] BURNOUT! (Game Over)");
            }
        }

        public void ResetBlock()
        {
            CurrentBlock = 0;
        }
    }
}
