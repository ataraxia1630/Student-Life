using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "RemoveCardService", menuName = "Đời Sinh Viên/Shop Services/Remove Card")]
    public class RemoveCardServiceData : ScriptableObject, IPurchasable
    {
        public string serviceName = "Quên Kiến Thức (Xóa Bài)";
        [TextArea] public string description = "Xóa 1 thẻ bài vĩnh viễn khỏi bộ bài.";
        public int price = 40;
        public Sprite icon; 

        public string ItemName => serviceName;
        public string ItemDescription => description;
        public int ItemPrice => price;
        public Sprite ItemIcon => icon;

        public void OnPurchased()
        {
            // Tạm thời MVP: Xóa lá bài đầu tiên. 
            // (Sau này đổi thành mở panel chọn bài giống Quán Cafe)
            if (PlayerInventory.Instance.masterDeck.Count > 0)
            {
                var removedCard = PlayerInventory.Instance.masterDeck[0];
                PlayerInventory.Instance.masterDeck.RemoveAt(0);
                Debug.Log($"[Service] Đã sử dụng dịch vụ: Xóa thẻ {removedCard.cardName}");
            }
        }
    }
}