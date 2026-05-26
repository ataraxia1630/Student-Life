using System.Collections.Generic;
using UnityEngine;

namespace DoiSinhVien.View
{
    public class CardPool : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private int initialPoolSize = 10;

        private Queue<CardView> pool = new();

        private void Awake()
        {
            for (int i = 0; i < initialPoolSize; i++)
            {
                CreateNewCard();
            }
        }

        private CardView CreateNewCard()
        {
            GameObject obj = Instantiate(cardPrefab, transform);
            obj.SetActive(false);
            CardView cardView = obj.GetComponent<CardView>();
            pool.Enqueue(cardView);
            return cardView;
        }

        public CardView GetCard()
        {
            if (pool.Count == 0)
            {
                CreateNewCard();
            }

            CardView cardView = pool.Dequeue();
            cardView.gameObject.SetActive(true);
            return cardView;
        }

        public void ReturnToPool(CardView cardView)
        {
            if (cardView == null) return;

            cardView.gameObject.SetActive(false);
            cardView.transform.localPosition = Vector3.zero;
            cardView.transform.localRotation = Quaternion.identity;
            cardView.transform.localScale = Vector3.one;

            pool.Enqueue(cardView);
        }
    }
}