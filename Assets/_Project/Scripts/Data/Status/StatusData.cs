using UnityEngine;
using DoiSinhVien.Combat;
using DoiSinhVien.Core;

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

        public virtual int ModifyDamage(PlayerCharacter player, CardInstance card, int currentDamage, int stack)
        {
            return currentDamage;
        }

        public virtual int ModifyCost(PlayerCharacter player, CardInstance card, int currentCost, int stack)
        {
            return currentCost;
        }

        public virtual int ModifyBlock(PlayerCharacter player, CardInstance card, int currentBlock, int stack)
        {
            return currentBlock;
        }

        public virtual void OnTurnStart(PlayerCharacter player, int stack) { }

        public virtual void OnTurnEnd(PlayerCharacter player, int stack) { }

    }
}