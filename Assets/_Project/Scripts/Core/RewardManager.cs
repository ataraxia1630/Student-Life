using System.Collections.Generic;
using UnityEngine;
using DoiSinhVien.Data;

namespace DoiSinhVien.Core
{
    public class RewardManager : MonoBehaviour
    {
        public static RewardManager Instance { get; private set; }

        [Header("Database")]
        public List<CardData> entireCardPool; 

        [Header("Player Meta")]
        public List<CardData> playerMasterDeck; 

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void GenerateCardRewards()
        {
            Debug.Log("\n--- PHẦN THƯỞNG CHIẾN THẮNG ---");
            List<CardData> offeredCards = new();

            for (int i = 0; i < 3; i++)
            {
                CardData randomCard = entireCardPool[Random.Range(0, entireCardPool.Count)];
                offeredCards.Add(randomCard);

                Debug.Log($"Lựa chọn {i + 1}: {randomCard.cardName}");
            }

            // TODO: Bật Canvas UI Reward hiển thị 3 thẻ này lên màn hình
            Debug.Log("Hãy chọn 1 thẻ để đưa vào Master Deck!");
        }

        // Hàm này sẽ được gọi khi bấm vào UI Thẻ bài trên màn hình Reward
        public void ClaimCard(CardData chosenCard)
        {
            playerMasterDeck.Add(chosenCard);
            Debug.Log($"Đã thêm [{chosenCard.cardName}] vào bộ bài vĩnh viễn!");

            // Kết thúc phòng, chuẩn bị mở Map cho phòng tiếp theo
        }

        public void SkipReward()
        {
            // +15 Credit theo GDD
            Debug.Log("Bỏ qua thẻ, nhận 15 Credit!");
        }
    }
}