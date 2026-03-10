using DoiSinhVien.Data;
using System;

namespace DoiSinhVien.Combat
{
    [Serializable] 
    public class CardInstance
    {
        public CardData Data { get; private set; }
        public int CurrentCost { get; set; }

        public CardInstance(CardData data)
        {
            Data = data;
            CurrentCost = data.manaCost; 
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