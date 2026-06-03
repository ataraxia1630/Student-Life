using DoiSinhVien.Core;
using DoiSinhVien.Data;
using System;
using UnityEngine;

namespace DoiSinhVien.Combat
{
    [Serializable] 
    public class CardInstance
    {
        public CardData Data { get; private set; }
        public int CurrentCost { get; private set; }
        public int CurrentDamage { get; private set; }
        public int CurrentBlock { get; private set; }
        public int CurrentMagicNumber { get; private set; }

        public int bonusDamage = 0;
        public int costModifier = 0;

        public CardInstance(CardData data)
        {
            Data = data;
            RecalculateStats(null);
        }

        public void RecalculateStats(PlayerCharacter player)
        {
            CurrentCost = Data.manaCost + costModifier;
            CurrentDamage = Data.baseDamage + bonusDamage;
            CurrentBlock = Data.baseBlock;
            CurrentMagicNumber = Data.baseMagicNumber;

            if (player != null)
            {
                foreach (var status in player.SortedStatuses)
                {
                    int stack = player.ActiveStatuses[status];

                    CurrentCost = status.ModifyCost(this, CurrentCost, stack);
                    CurrentDamage = status.ModifyDamage(this, CurrentDamage, stack);
                    CurrentBlock = status.ModifyBlock(this, CurrentBlock, stack);
                }
            }

            CurrentCost = Mathf.Max(0, CurrentCost);
            CurrentDamage = Mathf.Max(0, CurrentDamage);
            CurrentBlock = Mathf.Max(0, CurrentBlock);
        }

        // Hàm hỗ trợ AI MCTS: Clone lá bài này sang một vũ trụ mô phỏng khác
        public CardInstance Clone()
        {
            return new CardInstance(this.Data)
            {
                CurrentCost = this.CurrentCost
            };
        }
    }
}