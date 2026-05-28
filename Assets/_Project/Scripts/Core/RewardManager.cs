using System.Collections.Generic;
using UnityEngine;
using DoiSinhVien.Data;
using UnityEngine.SceneManagement;
using DoiSinhVien.View;

namespace DoiSinhVien.Core
{
    public class RewardManager : MonoBehaviour
    {
        public static RewardManager Instance { get; private set; }

        [Header("UI References")]
        public GameObject rewardPanel;
        public List<RewardCardView> rewardCardSlots;

        [Header("Database")]
        public List<CardData> entireCardPool;

        [Header("Scene Config")]
        public string mapSceneName = "Map";

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void GenerateCardRewards()
        {
            Debug.Log("\n--- PHẦN THƯỞNG CHIẾN THẮNG ---");
            rewardPanel.SetActive(true);

            List<CardData> pool = new(entireCardPool);

            bool isElite = RunManager.Instance != null && RunManager.Instance.currentRoomType == NodeType.Elite;

            if (isElite)
            {
                Debug.Log("Phòng Elite! Thưởng lớn: 50k & Thẻ bài xịn.");
                PlayerInventory.Instance.AddCredits(50);

                pool = entireCardPool.FindAll(c => c.rarity == CardRarity.Uncommon || c.rarity == CardRarity.Rare);
            }
            else
            {
                Debug.Log("Phòng Combat Thường! Thưởng cơ bản: 15k & Thẻ bài cơ bản.");
                PlayerInventory.Instance.AddCredits(15); 

                pool = entireCardPool.FindAll(c => c.rarity == CardRarity.Common || c.rarity == CardRarity.Uncommon);
            }

            if (pool.Count < 3)
            {
                pool = new List<CardData>(entireCardPool);
            }

            List<CardData> offeredCards = new();
            for (int i = 0; i < rewardCardSlots.Count; i++)
            {
                if (pool.Count > 0)
                {
                    int randomIndex = Random.Range(0, pool.Count);
                    offeredCards.Add(pool[randomIndex]);
                    pool.RemoveAt(randomIndex); 
                }
            }

            for (int i = 0; i < rewardCardSlots.Count; i++)
            {
                if (i < offeredCards.Count)
                {
                    rewardCardSlots[i].Setup(offeredCards[i]);
                    rewardCardSlots[i].gameObject.SetActive(true);
                }
                else rewardCardSlots[i].gameObject.SetActive(false);
            }
        }

        public void ClaimCard(CardData chosenCard)
        {
            PlayerInventory.Instance.AddCard(chosenCard);

            rewardPanel.SetActive(false);
            SceneManager.LoadScene(mapSceneName);
        }

        public void SkipReward()
        {
            rewardPanel.SetActive(false);
            SceneManager.LoadScene(mapSceneName);
        }
    }
}