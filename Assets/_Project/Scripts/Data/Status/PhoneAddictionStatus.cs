using UnityEngine;
using DoiSinhVien.Core;
using DoiSinhVien.Visual;

namespace DoiSinhVien.Data
{
    [CreateAssetMenu(fileName = "Status_PhoneAddiction", menuName = "Student Life/Status/Phone Addiction")]
    public class PhoneAddictionStatus : StatusData
    {
        [Tooltip("Kéo lá bài Lướt TikTok vào đây")]
        public CardData tiktokCard;

        public override void OnTurnStart(PlayerCharacter player, int stack)
        {
            var deck = CombatManager.Instance.deckManager;
            if (deck.hand.Count > 0 && tiktokCard != null)
            {
                int randomIdx = Random.Range(0, deck.hand.Count);
                var cardToReplace = deck.hand[randomIdx];

                // Giả sử bạn có hàm bỏ bài và thêm bài trong DeckManager.
                // Nếu chưa có, bạn viết thêm hàm RemoveCard(cardInstance) nhé.
                deck.hand.Remove(cardToReplace);

                // Thêm lá TikTok vào (Bạn cần code 1 hàm Instantiate UI cho nó ở HandView)
                // deck.AddCardToHand(tiktokCard);

                NotificationManager.Instance.ShowMessage("Mất tập trung: Vừa lướt TikTok!", Color.gray);
            }
        }
    }
}