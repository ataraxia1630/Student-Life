using UnityEngine;
using DoiSinhVien.Combat;

namespace DoiSinhVien.Data
{
    public abstract class StatusData : ScriptableObject
    {
        [Header("UI Info")]
        public string statusId;
        public string statusName;
        public Sprite icon;
        public Color iconColor = Color.white;
        public bool isDebuff; 

        [Header("Logic")]
        [Tooltip("Thứ tự tính toán: Số nhỏ tính trước (Cộng/Trừ), Số lớn tính sau (Nhân/Chia)")]
        public int priority = 0;

        public virtual int ModifyDamage(CardInstance card, int currentDamage, int stack)
        {
            return currentDamage;
        }

        public virtual int ModifyCost(CardInstance card, int currentCost, int stack)
        {
            return currentCost;
        }

        public virtual int ModifyBlock(CardInstance card, int currentBlock, int stack)
        {
            return currentBlock;
        }

    }
}