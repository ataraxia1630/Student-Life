using DoiSinhVien.Data;
using UnityEngine;

namespace DoiSinhVien.Core
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class PlayCardCommand : ICommand
    {
        private CardData _card;
        private ITargetable _target;
        private int _healthBeforePlay; 

        public PlayCardCommand(CardData card, ITargetable target)
        {
            _card = card;
            _target = target;
            _healthBeforePlay = target.CurrentHealth;
        }

        public void Execute()
        {
            Debug.Log($"\n>>> [COMMAND THỰC THI] Đánh bài: {_card.cardName} <<<");
            foreach (var effect in _card.effects)
            {
                if (effect != null) effect.Execute(_target);
            }
        }

        public void Undo()
        {
            _target.SetHealth(_healthBeforePlay);
            Debug.Log($"<<< [COMMAND HOÀN TÁC] Thu hồi lá {_card.cardName} <<<");
        }
    }
}