using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DoiSinhVien.Data;
using DoiSinhVien.Core;
using DoiSinhVien.View;
using UnityEngine.SceneManagement;

namespace DoiSinhVien.Core
{
    public class ShopManager : MonoBehaviour
    {
        public static ShopManager Instance { get; private set; }
        public string mapSceneName = "Map";

        [Header("Shop Containers")]
        [SerializeField] private Transform cardContainer;
        [SerializeField] private Transform relicContainer;

        [Header("Prefabs")]
        [SerializeField] private GameObject shopCardPrefab;
        [SerializeField] private GameObject shopRelicPrefab;

        [Header("Data Sources")]
        [SerializeField] private List<CardData> allCards;   
        [SerializeField] private List<RelicData> allRelics; 

        private List<ShopCardView> currentCardViews = new();
        private List<ShopRelicView> currentRelicViews = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            RefreshShop();
        }

        public void RefreshShop()
        {
            ClearCurrentItems();

            var randomCards = allCards.OrderBy(x => Random.value).Take(3).ToList();
            foreach (var card in randomCards)
            {
                var go = Instantiate(shopCardPrefab, cardContainer);
                var view = go.GetComponent<ShopCardView>();
                view.Setup(card);
                currentCardViews.Add(view);
            }

            var randomRelics = allRelics.OrderBy(x => Random.value).Take(2).ToList();
            foreach (var relic in randomRelics)
            {
                var go = Instantiate(shopRelicPrefab, relicContainer);
                var view = go.GetComponent<ShopRelicView>();
                view.Setup(relic);
                currentRelicViews.Add(view);
            }
        }

        private void ClearCurrentItems()
        {
            foreach (var item in currentCardViews) Destroy(item.gameObject);
            foreach (var item in currentRelicViews) Destroy(item.gameObject);

            currentCardViews.Clear();
            currentRelicViews.Clear();
        }

        public void LeaveShop()
        {
            SceneManager.LoadScene(mapSceneName);
        }
    }
}