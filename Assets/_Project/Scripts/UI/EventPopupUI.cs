using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DoiSinhVien.Data;
using DoiSinhVien.Core;

namespace DoiSinhVien.UI
{
    public class EventPopupUI : MonoBehaviour
    {
        public static EventPopupUI Instance { get; private set; }

        [Header("UI Elements")]
        public GameObject popupPanel;
        public TextMeshProUGUI titleText;

        [Header("Choice Buttons")]
        public Button[] choiceButtons;         
        public TextMeshProUGUI[] choiceTexts;  

        public bool IsOpen => popupPanel.activeSelf;
        private Action _onResumeCombat; 

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void ShowEvent(CombatEventData eventData, Action onResume)
        {
            titleText.text = eventData.eventName;
            _onResumeCombat = onResume;

            foreach (var btn in choiceButtons) btn.gameObject.SetActive(false);

            int choiceCount = Mathf.Min(eventData.choices.Count, choiceButtons.Length);
            for (int i = 0; i < choiceCount; i++)
            {
                int index = i; 
                EventChoice choice = eventData.choices[index];

                choiceButtons[index].gameObject.SetActive(true);
                choiceTexts[index].text = choice.choiceText;

                choiceButtons[index].onClick.RemoveAllListeners();
                choiceButtons[index].onClick.AddListener(() => OnChoiceSelected(choice));
            }

            popupPanel.SetActive(true);
        }

        private void OnChoiceSelected(EventChoice selectedChoice)
        {
            PlayerCharacter player = CombatManager.Instance.player;

            foreach (var effect in selectedChoice.consequences)
            {
                effect.Execute(player, player);
            }

            Debug.Log($"[Event] Đã chọn: {selectedChoice.choiceText}");

            popupPanel.SetActive(false);
            _onResumeCombat?.Invoke();
        }
    }
}