using UnityEngine;
using TMPro;
using DG.Tweening;

namespace DoiSinhVien.Visual
{
    public class NotificationManager : MonoBehaviour
    {
        public static NotificationManager Instance { get; private set; }

        [Header("UI References")]
        [Tooltip("Prefab chứa TextMeshProUGUI dùng để hiển thị chữ")]
        public GameObject notificationPrefab;

        [Tooltip("Panel hoặc Canvas để chứa các dòng thông báo (giúp nó nằm giữa màn hình)")]
        public Transform notificationContainer;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void ShowMessage(string message, Color? color = null)
        {
            if (notificationPrefab == null || notificationContainer == null) return;

            GameObject notifObj = Instantiate(notificationPrefab, notificationContainer);
            TMP_Text textComp = notifObj.GetComponent<TMP_Text>();

            if (textComp != null)
            {
                textComp.text = message;
                if (color.HasValue) textComp.color = color.Value;
            }

            notifObj.transform.localScale = Vector3.zero; 

            Sequence seq = DOTween.Sequence();

            seq.Append(notifObj.transform.DOScale(Vector3.one * 1.2f, 0.15f).SetEase(Ease.OutQuad));
            seq.Append(notifObj.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InOutQuad));

            seq.Join(notifObj.transform.DOLocalMoveY(150f, 1.2f).SetRelative(true).SetEase(Ease.OutCubic));

            if (textComp != null)
            {
                seq.Join(textComp.DOFade(0f, 0.7f).SetDelay(0.5f));
            }

            seq.OnComplete(() => Destroy(notifObj));
        }
    }
}