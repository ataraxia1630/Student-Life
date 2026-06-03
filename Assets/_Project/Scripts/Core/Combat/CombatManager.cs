using System.Collections.Generic;
using UnityEngine;
using DoiSinhVien.Data;
using DoiSinhVien.Combat;
using DoiSinhVien.View;
using System.Collections;
using DoiSinhVien.UI;
using DoiSinhVien.Visual;
using System.Linq;

namespace DoiSinhVien.Core
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager Instance { get; private set; }

        [Header("Logic System")]
        public DeckManager deckManager;

        [Header("Enemies on Scene")]
        public List<EnemyController> activeEnemies = new();

        [Header("Player Stats")]
        public PlayerCharacter player;
        public int maxEnergy = 3;
        public int currentEnergy;

        [Header("View System")]
        public HandView handView;

        [Header("Combat State")]
        public int currentTurn = 1;

        public CombatState CurrentState { get; private set; }

        public int pendingDiscardCount = 0;

        private CardType lastCardType;
        private int consecutiveCardCount = 0;
        public StatusData nextTurnEnergyStatus;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void StartCombat(List<EnemyController> spawnedEnemies)
        {
            activeEnemies = spawnedEnemies;

            deckManager = new();
            deckManager.InitializeDeck(PlayerInventory.Instance.masterDeck);
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
                    GameEvents.OnCombatStart?.Invoke();
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
                    GameEvents.OnCombatWin?.Invoke();
                    StartCoroutine(HandleVictoryRoutine());
                    break;
            }
        }

        private IEnumerator TurnStartSequence()
        {
            GameEvents.OnTurnStart?.Invoke();
            Debug.Log("[TEST] Đã phát OnTurnStart.");

            if (EventPopupUI.Instance != null)
            {
                yield return new WaitUntil(() => !EventPopupUI.Instance.IsOpen);
            }

            yield return StartCoroutine(StartHookCoroutine());

            yield return StartCoroutine(EnemyIntentRoutine());

            yield return StartCoroutine(DrawCardsRoutine(5));

            ChangeState(CombatState.Player_Turn_Active);
        }

        private IEnumerator StartHookCoroutine()
        {
            player.TriggerTurnStartHooks();
            yield return new WaitForSeconds(0.2f);
        }

        private IEnumerator DrawCardsRoutine(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                deckManager.DrawCard(1);
                yield return new WaitForSeconds(0.2f); 
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
            GameEvents.OnTurnEnd?.Invoke();
            Debug.Log("[TEST] Đã phát OnTurnEnd.");

            if (EventPopupUI.Instance != null)
            {
                yield return new WaitUntil(() => !EventPopupUI.Instance.IsOpen);
            }

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
            // HandView tự động xóa bài thông qua Event OnCardDiscarded lúc DiscardHand chạy!
            player.ResetBlock();
            player.TriggerTurnEndHooks();
            yield return new WaitForSeconds(0.5f);
            currentTurn++;
            ChangeState(CombatState.Player_Turn_Start);
        }


        public void OnEndTurn()
        {
            if (CurrentState != CombatState.Player_Turn_Active) return;
            ChangeState(CombatState.Enemy_Turn_Active);
        }

        public bool TryPlayCard(CardView cardView, GameObject explicitTarget = null)
        {
            if (CurrentState != CombatState.Player_Turn_Active) return false;

            CardInstance cardToPlay = cardView.LogicCard;

            if (cardToPlay.Data.isUnplayable)
            {
                Debug.LogWarning("!!! Đây là bài Rác, không thể đánh ra !!!");
                NotificationManager.Instance.ShowMessage("!!! Đây là bài Rác, không thể đánh ra !!!", Color.red);
                return false;
            }

            if (currentEnergy < cardToPlay.CurrentCost)
            {
                Debug.LogWarning("!!! Thiếu Energy, không thể đánh lá này !!!");
                NotificationManager.Instance.ShowMessage("!!! Thiếu Energy, không thể đánh lá này !!!", Color.yellow);
                return false;
            }

            bool hasProcrastination = player.ActiveStatuses.Keys.Any(s => s is ProcrastinationStatus);
            if (hasProcrastination && deckManager.hand.Count == 1)
            {
                NotificationManager.Instance.ShowMessage("Bệnh Trì Hoãn: Không thể đánh lá cuối cùng!", Color.red);
                return false;
            }

            EnemyController targetEnemy = null;

            if (explicitTarget != null)
            {
                targetEnemy = explicitTarget.GetComponent<EnemyController>();
            }
            else targetEnemy = activeEnemies.Find(e => e.CurrentHealth > 0);

            if (targetEnemy == null) return false;

            currentEnergy -= cardToPlay.CurrentCost;
            ICommand playCmd = new PlayCardCommand(cardToPlay.Data, player, targetEnemy, cardToPlay);
            playCmd.Execute();

            deckManager.PlayCardFromHand(cardToPlay);
            GameEvents.OnCardPlayed?.Invoke(cardToPlay);

            // Flow state
            if (cardToPlay.Data.type == lastCardType) consecutiveCardCount++;
            else { consecutiveCardCount = 1; lastCardType = cardToPlay.Data.type; }

            if (consecutiveCardCount == 3)
            {
                player.AddStatus(nextTurnEnergyStatus, 1);
                NotificationManager.Instance.ShowMessage("FLOW STATE: +1 Energy hiệp sau!", Color.yellow);
                consecutiveCardCount = 0; 
            }
            

            bool isAllEnemiesDead = activeEnemies.TrueForAll(e => e.CurrentHealth <= 0);
            if (isAllEnemiesDead)
            {
                Debug.Log("TOÀN BỘ QUÁI ĐÃ BỊ FIX! CHIẾN THẮNG!");
                ChangeState(CombatState.Combat_Win);
            }
            return true;
        }

        private IEnumerator HandleVictoryRoutine()
        {
            yield return new WaitForSeconds(1f);
            deckManager.DiscardHand();

            if (RewardManager.Instance != null)
            {
                RewardManager.Instance.GenerateCardRewards();
            }
        }

        public void StartDiscarding(int amount)
        {
            if (deckManager.hand.Count == 0) return;

            pendingDiscardCount = Mathf.Min(amount, deckManager.hand.Count);
            ChangeState(CombatState.Player_Discarding);
        }

        public void DiscardSelectedCard(CardView cardView)
        {
            deckManager.DiscardSpecificCardFromHand(cardView.LogicCard);

            pendingDiscardCount--;

            if (pendingDiscardCount <= 0)
            {
                ChangeState(CombatState.Player_Turn_Active);
            }
        }

        public void ConsumeEnergy(int amount)
        {
            currentEnergy = Mathf.Max(0, currentEnergy - amount);
            Debug.Log($"[CombatManager] Đã tiêu thụ {amount} Energy. Energy còn lại: {currentEnergy}");
        }
    }
}