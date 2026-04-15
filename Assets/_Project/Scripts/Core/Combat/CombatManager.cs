using System.Collections.Generic;
using UnityEngine;
using DoiSinhVien.Data;
using DoiSinhVien.Combat;
using DoiSinhVien.View;
using System.Collections;

namespace DoiSinhVien.Core
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager Instance { get; private set; }

        [Header("Logic System")]
        public DeckManager deckManager;
        public List<CardData> starterDeckData;

        [Header("Enemies on Scene")]
        public List<EnemyController> activeEnemies = new();

        [Header("Player Stats")]
        public PlayerCharacter player;
        public int maxEnergy = 3;
        public int currentEnergy;

        [Header("View System")]
        public HandView handView;
        public GameObject cardPrefab;

        [Header("Combat State")]
        public int currentTurn = 1;

        public CombatState CurrentState { get; private set; }

        private readonly Dictionary<CardInstance, CardView> cardViewMap = new();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void StartCombat(List<EnemyController> spawnedEnemies)
        {
            activeEnemies = spawnedEnemies;

            deckManager = new();
            deckManager.InitializeDeck(starterDeckData);
            player.Initialize();

            Debug.Log($"BẮT ĐẦU TRẬN ĐẤU! Số lượng quái: {activeEnemies.Count}");
            ChangeState(CombatState.Initialize);
        }    

        public void ChangeState(CombatState newState)
        {
            CurrentState = newState;
            Debug.Log($"\n[STATE MACHINE] ---> {newState}");

            switch (newState)
            {
                case CombatState.Initialize:
                    currentTurn = 1;
                    ChangeState(CombatState.Player_Turn_Start);
                    break;

                case CombatState.Player_Turn_Start:
                    currentEnergy = maxEnergy;
                    StartCoroutine(TurnStartSequence());
                    break;

                case CombatState.Player_Turn_Active:
                    Debug.Log("=> TỚI LƯỢT BẠN! Hãy đánh bài hoặc bấm nút End Turn.");
                    break;

                case CombatState.Enemy_Turn_Active:
                    StartCoroutine(EnemyActionRoutine()); 
                    break;

                case CombatState.Turn_End_Cleanup:
                    StartCoroutine(CleanupRoutine()); 
                    break;
                case CombatState.Combat_Win:
                    StartCoroutine(HandleVictoryRoutine());
                    break;
            }
        }

        private IEnumerator TurnStartSequence()
        {
            yield return StartCoroutine(EnemyIntentRoutine());

            yield return StartCoroutine(DrawCardsRoutine(5));

            ChangeState(CombatState.Player_Turn_Active);
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
        }

        private IEnumerator EnemyIntentRoutine()
        {
            foreach (var enemy in activeEnemies)
            {
                if (enemy.CurrentHealth > 0)
                {
                    enemy.DetermineNextIntent();
                }
            }
            yield return new WaitForSeconds(1f);
        }

        private IEnumerator EnemyActionRoutine()
        {
            foreach (var enemy in activeEnemies)
            {
                if (enemy.CurrentHealth > 0)
                {
                    enemy.ResetBlock();
                    enemy.ExecuteIntent(player);
                    yield return new WaitForSeconds(0.8f);
                }
            }
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

            player.ResetBlock();

            yield return new WaitForSeconds(0.5f);

            currentTurn++;

            ChangeState(CombatState.Player_Turn_Start);
        }


        public void OnEndTurn()
        {
            if (CurrentState != CombatState.Player_Turn_Active) return;
            ChangeState(CombatState.Enemy_Turn_Active);
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

            EnemyController targetEnemy = activeEnemies.Find(e => e.CurrentHealth > 0);

            if (targetEnemy == null) return;

            currentEnergy -= cardToPlay.CurrentCost;
            ICommand playCmd = new PlayCardCommand(cardToPlay.Data, player, targetEnemy);
            playCmd.Execute();
            deckManager.PlayCardFromHand(cardToPlay);

            StartCoroutine(handView.RemoveCard(cardView));
            cardViewMap.Remove(cardToPlay);
            Destroy(cardView.gameObject);

            bool isAllEnemiesDead = activeEnemies.TrueForAll(e => e.CurrentHealth <= 0);
            if (isAllEnemiesDead)
            {
                Debug.Log("TOÀN BỘ QUÁI ĐÃ BỊ FIX! CHIẾN THẮNG!");
                ChangeState(CombatState.Combat_Win);
            }
        }

        private IEnumerator HandleVictoryRoutine()
        {
            yield return new WaitForSeconds(1f); 

            foreach (var view in cardViewMap.Values)
            {
                if (view != null) Destroy(view.gameObject);
            }
            cardViewMap.Clear();
            handView.cards.Clear();

            if (RewardManager.Instance != null)
            {
                RewardManager.Instance.GenerateCardRewards();
            }
        }
    }
}