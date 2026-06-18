using UnityEngine;
using System.Collections.Generic;
using DoiSinhVien.Core;
using DoiSinhVien.Data;

namespace DoiSinhVien.UI
{
    public class CombatRelicUI : MonoBehaviour
    {
        [Header("References")]
        public GameObject relicIconPrefab; 
        public Transform relicContainer;   

        private List<GameObject> spawnedIcons = new List<GameObject>();

        private void OnEnable()
        {
            GameEvents.OnCombatStart += RedrawRelics;

            // Nếu có Event nhận Relic GIỮA TRẬN, hãy thêm Event OnRelicAdded và lắng nghe ở đây
        }

        private void OnDisable()
        {
            GameEvents.OnCombatStart -= RedrawRelics;
        }

        public void RedrawRelics()
        {
            if (PlayerInventory.Instance == null) return;

            foreach (var iconObj in spawnedIcons)
            {
                Destroy(iconObj);
            }
            spawnedIcons.Clear();

            List<RelicData> ownedRelics = PlayerInventory.Instance.ownedRelics;

            foreach (var relicData in ownedRelics)
            {
                GameObject newIconObj = Instantiate(relicIconPrefab, relicContainer);
                spawnedIcons.Add(newIconObj);

                RelicItemView iconView = newIconObj.GetComponent<RelicItemView>();
                if (iconView != null)
                {
                    iconView.Setup(relicData);
                }
            }
        }
    }
}