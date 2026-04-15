using UnityEngine;
using DoiSinhVien.Core;

namespace DoiSinhVien.Data
{
    public enum BuffType { BonusDamage, BonusDraw, DoubleNextCard }

    [CreateAssetMenu(fileName = "ApplyBuff_", menuName = "Student Life/Effects/Apply Buff")]
    public class ApplyPlayerBuffEffect : CardEffectData
    {
        public BuffType buffType;
        public int value;

        public override void Execute(ITargetable self, ITargetable target)
        {
            if (self is PlayerCharacter player)
            {
                switch (buffType)
                {
                    case BuffType.BonusDamage:
                        player.bonusAttackDamage += value;
                        Debug.Log($"[Power] 10x Developer kích hoạt! +{value} DMG cho mọi thẻ Attack.");
                        break;
                    case BuffType.BonusDraw:
                        player.bonusDrawPerTurn += value;
                        Debug.Log($"[Power] Agile Mindset kích hoạt! Rút thêm {value} bài mỗi đầu lượt.");
                        break;
                    case BuffType.DoubleNextCard:
                        player.isNextCardDoubled = true;
                        Debug.Log($"[Skill] Pair Programming! Lá bài tiếp theo sẽ x2 hiệu ứng.");
                        break;
                }
            }
        }
    }
}