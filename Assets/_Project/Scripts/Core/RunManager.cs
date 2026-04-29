using UnityEngine;
using DoiSinhVien.Data;
using System.Collections.Generic;

namespace DoiSinhVien.Core
{
    public class RunManager : MonoBehaviour
    {
        public static RunManager Instance { get; private set; }

        public EncounterBlueprint pendingEncounter;

        public MapGenerator mapGenerator;
        public Dictionary<int, MapNodeData> CurrentMap => mapGenerator.MapGraph;

        public int currentNodeId = 0;


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

        private void Start()
        {
            mapGenerator.GenerateNewMap();
        }
    }
}