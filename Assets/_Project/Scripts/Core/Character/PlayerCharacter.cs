using DoiSinhVien.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DoiSinhVien.Core
{
    public class PlayerCharacter : MonoBehaviour, ITargetable
    {
        public int MaxHealth => PlayerInventory.Instance.maxHealth;
        public int CurrentHealth { get; private set; }
        public int CurrentBlock { get; private set; }

        public Dictionary<StatusData, int> ActiveStatuses { get; private set; } = new();
        public List<StatusData> SortedStatuses { get; private set; } = new();

        public Action OnStatusChanged;

        public void Initialize()
        {
            CurrentHealth = PlayerInventory.Instance.currentHealth;
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

                if (CurrentHealth <= 0)
                {
                    CurrentHealth = 0;
                    Debug.Log("ÁP LỰC QUÁ TẢI! SINH VIÊN ĐÃ GỤC NGÃ!");

                    CombatManager.Instance.ChangeState(CombatState.Combat_Lose);
                } 
                    
            }
        }

        public void Heal(int amount)
        {
            if (amount > 0) CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
            else CurrentHealth = Mathf.Max(CurrentHealth + amount, 0);
        } 
            
        public void ResetBlock()
        {
            CurrentBlock = 0;
        }

        public void AddStatus(StatusData statusObj, int amount)
        {
            if (statusObj == null) return;

            if (!ActiveStatuses.ContainsKey(statusObj))
            {
                ActiveStatuses[statusObj] = 0;
                SortedStatuses.Add(statusObj); 
            }

            ActiveStatuses[statusObj] += amount;

            if (ActiveStatuses[statusObj] <= 0)
            {
                ActiveStatuses.Remove(statusObj);
                SortedStatuses.Remove(statusObj); 
            }

            SortedStatuses = SortedStatuses.OrderBy(s => s.priority).ToList();

            OnStatusChanged?.Invoke();
        }

        public void TriggerTurnStartHooks()
        {
            var statuses = SortedStatuses.ToList();
            foreach (var status in statuses)
            {
                if (ActiveStatuses.ContainsKey(status))
                {
                    status.OnTurnStart(this, ActiveStatuses[status]);
                }
            }
        }

        public void TriggerTurnEndHooks()
        {
            var statuses = SortedStatuses.ToList();
            foreach (var status in statuses)
            {
                if (ActiveStatuses.ContainsKey(status))
                {
                    status.OnTurnEnd(this, ActiveStatuses[status]);
                }
            }
        }
    }
}
