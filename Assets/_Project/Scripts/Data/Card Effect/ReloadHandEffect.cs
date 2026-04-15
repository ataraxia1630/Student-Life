using UnityEngine;
using DoiSinhVien.Core;
using System.Collections.Generic;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "ReloadHand", menuName = "Student Life/Effects/Reload Hand")]
    public class ReloadHandEffect : CardEffectData
    {
        public override void Execute(ITargetable self, ITargetable target)
        {
            var deck = CombatManager.Instance.deckManager;

            int cardsToDraw = deck.hand.Count - 1;

            deck.drawPile.AddRange(deck.hand);
            deck.hand.Clear();
            deck.ShuffleDrawPile();

            deck.DrawCard(cardsToDraw);
            Debug.Log($"[Effect] Git Reset --hard! Xào lại {cardsToDraw} lá bài trên tay.");
        }
    }
}