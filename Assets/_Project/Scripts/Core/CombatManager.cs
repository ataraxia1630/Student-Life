using System.Collections.Generic;
using UnityEngine;
using DoiSinhVien.Data;
using DoiSinhVien.Combat;
using DoiSinhVien.View;
using UnityEngine.InputSystem;

namespace DoiSinhVien.Core
{
    public class CombatManager : MonoBehaviour
    {
        [Header("Logic System")]
        public DeckManager deckManager;
        public List<CardData> starterDeckData; 
        public DummyEnemy currentEnemy; 

        [Header("View System")]
        public HandView handView;
        public GameObject cardPrefab; 

        private readonly Dictionary<CardInstance, CardView> cardViewMap = new();

        private void Start()
        {
            deckManager = new DeckManager();
            deckManager.InitializeDeck(starterDeckData);

            Debug.Log($"Đã nạp {starterDeckData.Count} lá bài vào Draw Pile.");
        }

        private void Update()
        {
            if (Keyboard.current == null) return;

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                DrawCardVisual();
            }

            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                PlayCardVisual(0);
            }

            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                ReshuffleDiscardPile();
            }
        }


        private void DrawCardVisual()
        {
            if (deckManager.drawPile.Count == 0)
            {
                Debug.Log("Hết bài trong Draw Pile! Bấm R để xáo lại.");
                return;
            }

            deckManager.DrawCard(1);
            CardInstance newlyDrawnCard = deckManager.hand[deckManager.hand.Count - 1]; 

            GameObject cardObj = Instantiate(cardPrefab, handView.transform.position, Quaternion.identity);
            CardView newCardView = cardObj.GetComponent<CardView>();

            newCardView.Setup(newlyDrawnCard); 
            cardViewMap.Add(newlyDrawnCard, newCardView); 

            StartCoroutine(handView.AddCard(newCardView)); 
        }

        private void PlayCardVisual(int indexInHand)
        {
            if (indexInHand >= deckManager.hand.Count) return;

            CardInstance cardToPlay = deckManager.hand[indexInHand];
            CardView viewToPlay = cardViewMap[cardToPlay];

            ICommand playCmd = new PlayCardCommand(cardToPlay.Data, currentEnemy);
            playCmd.Execute(); 

            deckManager.PlayCardFromHand(cardToPlay);

            StartCoroutine(handView.RemoveCard(viewToPlay));
            cardViewMap.Remove(cardToPlay);

            Destroy(viewToPlay.gameObject);
        }

        private void ReshuffleDiscardPile()
        {
            if (deckManager.discardPile.Count == 0) return;

            deckManager.drawPile.AddRange(deckManager.discardPile);
            deckManager.discardPile.Clear();
            deckManager.ShuffleDrawPile();

            Debug.Log($"Đã xáo lại bài! Draw Pile hiện có: {deckManager.drawPile.Count} lá.");
        }
    }
}