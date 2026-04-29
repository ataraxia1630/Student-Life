using System.Collections.Generic;
using UnityEngine;
using DoiSinhVien.Data;

namespace DoiSinhVien.Core
{
    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory Instance { get; private set; }

        [Header("Currencies & Stats")]
        public int currentCredits = 50;
        public int currentHealth = 100;
        public int maxHealth = 100;

        [Header("Collections")]
        public List<CardData> masterDeck = new();
        public List<RelicData> ownedRelics = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else Destroy(gameObject);
        }

        private void Start()
        {
            GameEvents.OnCreditsChanged?.Invoke(currentCredits);
        }

        public void AddCredits(int amount)
        {
            currentCredits += amount;
            GameEvents.OnCreditsChanged?.Invoke(currentCredits); 
            Debug.Log($"[Inventory] Nhận {amount}k. Số dư: {currentCredits}k");
        }

        public bool SpendCredits(int amount)
        {
            if (currentCredits >= amount)
            {
                currentCredits -= amount;
                GameEvents.OnCreditsChanged?.Invoke(currentCredits); 
                Debug.Log($"[Inventory] Tiêu {amount}k. Số dư: {currentCredits}k");
                return true;
            }
            Debug.LogWarning("[Inventory] Không đủ tiền!");
            return false;
        }

        public void AddCard(CardData card)
        {
            masterDeck.Add(card);
            Debug.Log($"[Inventory] Đã thêm thẻ: {card.cardName}");
        }

        public void AddRelic(RelicData relic)
        {
            ownedRelics.Add(relic);
            Debug.Log($"[Inventory] Đã nhận Relic: {relic.relicName}");
        }
    }
}