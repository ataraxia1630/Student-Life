using UnityEngine;
using DoiSinhVien.Config;

namespace DoiSinhVien.Core
{
    public class ThemeManager : MonoBehaviour
    {
        public static ThemeManager Instance { get; private set; }

        [Header("Global Themes")]
        public CardVisualSettings cardVisuals;
        public CombatVisualSettings combatVisuals;

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

        //function ChangeTheme(CardVisualSettings newTheme)
    }
}