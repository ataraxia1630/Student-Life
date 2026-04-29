using System;
using DoiSinhVien.Combat; 

namespace DoiSinhVien.Core
{
    public static class GameEvents
    {
        public static Action OnCombatStart;
        public static Action OnTurnStart;
        public static Action OnTurnEnd;
        public static Action<CardInstance> OnCardPlayed; 
        public static Action<int> OnPlayerTookDamage;    
        public static Action OnCombatWin;
        public static Action<int> OnCreditsChanged;
    }
}