using UnityEngine;
using DoiSinhVien.Data;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace DoiSinhVien.Core
{
    public class CombatTester : MonoBehaviour
    {
        [Header("Setup Test")]
        public CardData cardToPlay; 
        public DummyEnemy targetEnemy;

        private Stack<ICommand> commandHistory = new();


        private void Update()
        {
            if (Keyboard.current == null) return;
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                if (cardToPlay == null || targetEnemy == null) return;

                ICommand playCmd = new PlayCardCommand(cardToPlay, targetEnemy);

                playCmd.Execute();

                commandHistory.Push(playCmd);
            }

            if (Keyboard.current.backspaceKey.wasPressedThisFrame)
            {
                if (commandHistory.Count > 0)
                {
                    ICommand lastCommand = commandHistory.Pop();
                    lastCommand.Undo();
                }
                else
                {
                    Debug.Log("Không có hành động nào để Hoàn tác!");
                }
            }
        }
    }
}