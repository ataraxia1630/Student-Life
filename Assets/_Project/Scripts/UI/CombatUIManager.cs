using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DoiSinhVien.Core;

namespace DoiSinhVien.UI
{
    public class CombatUIManager : MonoBehaviour
    {
        [Header("Top Bar: Game State")]
        public TextMeshProUGUI turnText;       
        public TextMeshProUGUI gameStateText;  

        [Header("Bottom Left: Energy & Draw Pile")]
        public TextMeshProUGUI energyText;     
        public TextMeshProUGUI drawPileText;   

        [Header("Bottom Right: End Turn & Discard Pile")]
        public Button endTurnButton;
        public TextMeshProUGUI discardPileText; 

        private void Start()
        {
            if (endTurnButton != null)
            {
                endTurnButton.onClick.AddListener(() => {
                    CombatManager.Instance.OnEndTurn();
                });
            }
        }

        private void Update()
        {
            var cm = CombatManager.Instance;
            if (cm == null || cm.deckManager == null) return;

            turnText.text = $"Turn: {cm.currentTurn}";

            if (cm.CurrentState == CombatState.Player_Turn_Active)
                gameStateText.text = "LƯỢT CỦA BẠN";
            else if (cm.CurrentState == CombatState.Enemy_Turn_Active)
                gameStateText.text = "KẺ THÙ TẤN CÔNG";
            else
                gameStateText.text = "..."; 

            energyText.text = $"{cm.currentEnergy} / {cm.maxEnergy}";
            drawPileText.text = cm.deckManager.drawPile.Count.ToString();

            discardPileText.text = cm.deckManager.discardPile.Count.ToString();

            endTurnButton.interactable = (cm.CurrentState == CombatState.Player_Turn_Active);
        }
    }
}