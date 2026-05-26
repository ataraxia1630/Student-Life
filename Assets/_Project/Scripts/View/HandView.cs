using DG.Tweening;
using DoiSinhVien.Combat;
using DoiSinhVien.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace DoiSinhVien.View
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] private SplineContainer splineContainer;
        [SerializeField] private GameObject cardPrefab; 

        public List<CardView> cards = new();

        private void OnEnable()
        {
            GameEvents.OnCardDrawn += AddCardToHandVisual;
            GameEvents.OnCardDiscarded += RemoveCardFromHandVisual;
            GameEvents.OnCardPlayed += RemoveCardFromHandVisual;
        }

        private void OnDisable()
        {
            GameEvents.OnCardDrawn -= AddCardToHandVisual;
            GameEvents.OnCardDiscarded -= RemoveCardFromHandVisual;
            GameEvents.OnCardPlayed -= RemoveCardFromHandVisual;
        }

        private void AddCardToHandVisual(CardInstance newCard)
        {
            GameObject cardObj = Instantiate(cardPrefab, transform.position, Quaternion.identity, transform);
            CardView newCardView = cardObj.GetComponent<CardView>();
            newCardView.Setup(newCard);

            StartCoroutine(AddCard(newCardView));
        }

        private void RemoveCardFromHandVisual(CardInstance cardToRemove)
        {
            CardView cardView = cards.Find(c => c.LogicCard == cardToRemove);
            if (cardView == null) return;

            cards.Remove(cardView);
            StartCoroutine(RemoveCard(cardView));
        }

        public IEnumerator AddCard(CardView cardView)
        {
            cards.Add(cardView);
            yield return UpdateCardPositions(0.15f);
        }

        public IEnumerator RemoveCard(CardView cardView)
        {
            yield return UpdateCardPositions(0.15f);
            Destroy(cardView.gameObject, 0.1f);
        }

        private IEnumerator UpdateCardPositions(float duration)
        {
            if (cards.Count == 0) yield break;
            float cardSpacing = 1f / (cards.Count + 1);
            float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2f;

            Spline spline = splineContainer.Spline;
            for (int i = 0; i < cards.Count; i++)
            {
                float position = firstCardPosition + i * cardSpacing;
                Vector3 splinePos = spline.EvaluatePosition(position);
                Vector3 forward = spline.EvaluateTangent(position);
                Vector3 up = spline.EvaluateUpVector(position);
                Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);

                cards[i].transform.DOMove(splinePos + transform.position + 0.01f * i * Vector3.back, duration);
                cards[i].transform.DORotate(rotation.eulerAngles, duration);
            }
            yield return new WaitForSeconds(duration);
        }
    }
}