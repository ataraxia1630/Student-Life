using System.Collections.Generic;
using UnityEngine;
using DoiSinhVien.Data;
using DoiSinhVien.Combat;
using DoiSinhVien.View;
using UnityEngine.InputSystem;
using System.Collections;

namespace DoiSinhVien.Core
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager Instance { get; private set; }

        [Header("Logic System")]
        public DeckManager deckManager;
        public List<CardData> starterDeckData; 
        public DummyEnemy currentEnemy;

        [Header("Player Stats")]
        public int maxEnergy = 3;
        public int currentEnergy;

        [Header("View System")]
        public HandView handView;
        public GameObject cardPrefab;

        public CombatState CurrentState { get; private set; }

        private readonly Dictionary<CardInstance, CardView> cardViewMap = new();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            deckManager = new DeckManager();
            deckManager.InitializeDeck(starterDeckData);

            Debug.Log($"Đã nạp {starterDeckData.Count} lá bài vào Draw Pile.");
            ChangeState(CombatState.Initialize);
        }

        public void ChangeState(CombatState newState)
        {
            CurrentState = newState;
            Debug.Log($"\n[STATE MACHINE] ---> {newState}");

            switch (newState)
            {
                case CombatState.Initialize:
                    ChangeState(CombatState.Player_Turn_Start);
                    break;

                case CombatState.Player_Turn_Start:
                    currentEnergy = maxEnergy;
                    StartCoroutine(DrawCardsRoutine(5)); 
                    break;

                case CombatState.Player_Turn_Active:
                    Debug.Log("=> TỚI LƯỢT BẠN! Hãy đánh bài hoặc bấm nút End Turn.");
                    break;

                case CombatState.Enemy_Turn_Start:
                    ChangeState(CombatState.Enemy_Turn_Active);
                    break;

                case CombatState.Enemy_Turn_Active:
                    StartCoroutine(EnemyActionRoutine()); 
                    break;

                case CombatState.Turn_End_Cleanup:
                    StartCoroutine(CleanupRoutine()); 
                    break;
            }
        }

        private IEnumerator DrawCardsRoutine(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                deckManager.DrawCard(1);

                if (deckManager.hand.Count == 0) continue;

                CardInstance drawnCard = deckManager.hand[deckManager.hand.Count - 1];

                GameObject cardObj = Instantiate(cardPrefab, handView.transform.position, Quaternion.identity);
                CardView newCardView = cardObj.GetComponent<CardView>();
                newCardView.Setup(drawnCard);
                cardViewMap.Add(drawnCard, newCardView);

                yield return StartCoroutine(handView.AddCard(newCardView));
            }

            ChangeState(CombatState.Player_Turn_Active);
        }

        private IEnumerator EnemyActionRoutine()
        {
            Debug.Log($"[Enemy] {currentEnemy.enemyName} đang vận nội công...");
            yield return new WaitForSeconds(1.5f); 
            Debug.Log($"[Enemy] Hủy diệt!");

            ChangeState(CombatState.Turn_End_Cleanup);
        }

        private IEnumerator CleanupRoutine()
        {
            deckManager.DiscardHand(); 

            foreach (var view in cardViewMap.Values)
            {
                if (view != null) Destroy(view.gameObject);
            }
            cardViewMap.Clear();

            handView.cards.Clear();

            yield return new WaitForSeconds(0.5f); 

            ChangeState(CombatState.Player_Turn_Start);
        }


        public void OnEndTurn()
        {
            if (CurrentState != CombatState.Player_Turn_Active) return;
            ChangeState(CombatState.Enemy_Turn_Start);
        }

        public void TryPlayCard(CardView cardView)
        {
            if (CurrentState != CombatState.Player_Turn_Active) return;

            CardInstance cardToPlay = cardView.LogicCard;

            if (currentEnergy < cardToPlay.CurrentCost)
            {
                Debug.LogWarning("!!! Thiếu Energy, không thể đánh lá này !!!");
                return;
            }

            currentEnergy -= cardToPlay.CurrentCost;
            ICommand playCmd = new PlayCardCommand(cardToPlay.Data, currentEnemy);
            playCmd.Execute();
            deckManager.PlayCardFromHand(cardToPlay);
            
            StartCoroutine(handView.RemoveCard(cardView));
            cardViewMap.Remove(cardToPlay);
            Destroy(cardView.gameObject);
        }
    }
}