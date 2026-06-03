using System;
using UnityEngine;
using DoiSinhVien.Combat; 
using DoiSinhVien.Visual;

namespace DoiSinhVien.Core
{
    public static class GameEvents
    {
        public static Action OnCombatStart;
        public static Action OnTurnStart;
        public static Action OnTurnEnd;
        public static Action<int> OnPlayerTookDamage;    
        public static Action OnCombatWin;
        public static Action<int> OnCreditsChanged;

        public static Action<CardInstance> OnCardPlayed;
        public static Action<CardInstance> OnCardDrawn;
        public static Action<CardInstance> OnCardDiscarded;

        public static Action<Transform, int> OnEntityDamaged;
        public static Action<Transform, int> OnEntityGainedBlock;
        public static Action<Transform, VFXType> OnPlayVFX;
    }
}