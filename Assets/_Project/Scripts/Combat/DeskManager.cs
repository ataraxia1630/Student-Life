using DoiSinhVien.Data;
using System.Collections.Generic;
using UnityEngine;

namespace DoiSinhVien.Combat
{
    public class DeckManager
    {
        public List<CardInstance> drawPile = new();
        public List<CardInstance> hand = new();
        public List<CardInstance> discardPile = new();
        public List<CardInstance> exhaustPile = new();

        private const int MAX_HAND_SIZE = 7; 

        public void InitializeDeck(List<CardData> starterDeck)
        {
            drawPile.Clear();
            hand.Clear();
            discardPile.Clear();
            exhaustPile.Clear();

            foreach (var cardData in starterDeck)
            {
                drawPile.Add(new CardInstance(cardData));
            }

            ShuffleDrawPile();
        }

        // Fisher-Yates algorithm 
        public void ShuffleDrawPile()
        {
            System.Random rng = new System.Random();
            int n = drawPile.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                CardInstance value = drawPile[k];
                drawPile[k] = drawPile[n];
                drawPile[n] = value;
            }
            Debug.Log("[DeckManager] Đã xáo trộn Draw Pile.");
        }

        public void DrawCard(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                if (drawPile.Count == 0)
                {
                    if (discardPile.Count == 0)
                    {
                        Debug.LogWarning("[DeckManager] Hết bài để rút!");
                        return;
                    }
                    drawPile.AddRange(discardPile);
                    discardPile.Clear();
                    ShuffleDrawPile();
                    Debug.Log("[DeckManager] Đã tái chế Discard Pile thành Draw Pile mới.");
                }

                if (hand.Count >= MAX_HAND_SIZE)
                {
                    Debug.Log("[DeckManager] Đầy tay! Bài rút ra bị đưa thẳng vào Discard.");
                    CardInstance overdrawnCard = drawPile[0];
                    drawPile.RemoveAt(0);
                    discardPile.Add(overdrawnCard);
                    continue;
                }

                CardInstance drawnCard = drawPile[0];
                drawPile.RemoveAt(0);
                hand.Add(drawnCard);
                Debug.Log($"[DeckManager] Đã rút lá: {drawnCard.Data.cardName}");
            }
        }

        public void DiscardRandomCardFromHand(int discardAmount)
        {
            if (hand.Count == 0)
            {
                Debug.LogWarning("[DeckManager] Không có bài nào trên tay để bỏ!");
                return;
            }

            if (discardAmount > hand.Count)
            {
                Debug.LogWarning($"[DeckManager] Số lượng cần bỏ ({discardAmount}) lớn hơn số bài trên tay ({hand.Count}). Sẽ bỏ hết bài trên tay.");
                discardAmount = hand.Count;
            }

            System.Random rng = new System.Random();
            for (int i = 0; i < discardAmount; i++)
            {
                int index = rng.Next(hand.Count);
                CardInstance discardedCard = hand[index];
                hand.RemoveAt(index);
                discardPile.Add(discardedCard);
                Debug.Log($"[DeckManager] Đã bỏ lá: {discardedCard.Data.cardName} từ tay.");
            }
        }

        public void PlayCardFromHand(CardInstance card)
        {
            if (hand.Contains(card))
            {
                hand.Remove(card);

                if (card.Data.isExhaust)
                {
                    exhaustPile.Add(card);
                    Debug.Log($"[DeckManager] Thẻ {card.Data.cardName} đã bị Exhaust (Loại bỏ).");
                }
                else
                {
                    discardPile.Add(card);
                }
            }
        }

        public void DiscardHand()
        {
            discardPile.AddRange(hand);
            hand.Clear();
            Debug.Log("[DeckManager] Đã dọn dẹp bài trên tay vào Discard Pile.");
        }
    }
}