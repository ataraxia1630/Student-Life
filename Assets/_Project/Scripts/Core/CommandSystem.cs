using DoiSinhVien.Combat;
using DoiSinhVien.Data;
using DoiSinhVien.Visual;
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
        private ITargetable _self;
        private int _healthBeforePlay; 
        private CardInstance _cardInstance;

        public PlayCardCommand(CardData card, ITargetable self, ITargetable target, CardInstance cardInstance)
        {
            _card = card;
            _self = self;
            _target = target;
            _healthBeforePlay = target.CurrentHealth;
            _cardInstance = cardInstance;
        }

        public void Execute()
        {
            Debug.Log($"\n>>> [COMMAND THỰC THI] Đánh bài: {_card.cardName} <<<");
            foreach (var effect in _card.effects)
            {
                if (effect != null) effect.Execute(_self, _target, _cardInstance);
            }
        }

        public void Undo()
        {
            _target.SetHealth(_healthBeforePlay);
            Debug.Log($"<<< [COMMAND HOÀN TÁC] Thu hồi lá {_card.cardName} <<<");
        }
    }
}