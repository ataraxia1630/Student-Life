using UnityEngine;
using DoiSinhVien.Data;

namespace DoiSinhVien.Core
{
    public class RunManager : MonoBehaviour
    {
        public static RunManager Instance { get; private set; }

        [Header("Run State Data")]
        [Tooltip("Blueprint của phòng sắp đánh, truyền từ Map sang Combat")]
        public EncounterBlueprint pendingEncounter;

        // public int currentPlayerHealth;
        // public List<CardData> currentPlayerDeck;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}