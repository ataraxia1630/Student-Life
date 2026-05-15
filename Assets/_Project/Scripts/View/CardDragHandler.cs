using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DoiSinhVien.Core;
using DoiSinhVien.Data;
using DoiSinhVien.Combat;
using DoiSinhVien.View;
using DoiSinhVien.Visual;

namespace DoiSinhVien.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private CardView _cardView;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        private Vector2 _originalPosition;
        private TargetHighlight _currentTarget;
        public GameObject cardVisual;

        [Header("Drag Settings")]
        public float smartDropYThreshold = 300f; // Kéo bài qua mốc Y này là tự Smart Cast
        public Color invalidColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);

        private void Awake()
        {
            _cardView = GetComponent<CardView>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            if (cardVisual == null) cardVisual = GetComponentInChildren<SpriteRenderer>().gameObject;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (CombatManager.Instance.CurrentState != CombatState.Player_Turn_Active) return;

            _originalPosition = _rectTransform.anchoredPosition;
            _canvasGroup.blocksRaycasts = false; 
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (CombatManager.Instance.CurrentState != CombatState.Player_Turn_Active) return;

            transform.position = eventData.position;

            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            RaycastHit hit;

            TargetHighlight hitTarget = null;
            bool isHoveringValid = false;

            if (Physics.Raycast(ray, out hit))
            {
                hitTarget = hit.collider.GetComponent<TargetHighlight>();
                if (hitTarget != null)
                {
                    isHoveringValid = ValidateTarget(hitTarget.gameObject);
                }
            }

            if (_currentTarget != hitTarget)
            {
                if (_currentTarget != null) _currentTarget.Unhighlight();
                _currentTarget = hitTarget;
                if (_currentTarget != null && isHoveringValid) _currentTarget.HighlightAsValid();
            }

            if (hitTarget != null && !isHoveringValid) cardVisual.GetComponent<SpriteRenderer>().color = invalidColor;
            else cardVisual.GetComponent<SpriteRenderer>().color = Color.white;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (CombatManager.Instance.CurrentState != CombatState.Player_Turn_Active) return;

            _canvasGroup.blocksRaycasts = true; 
            cardVisual.GetComponent<SpriteRenderer>().color = Color.white;

            if (_currentTarget != null) _currentTarget.Unhighlight();

            bool isCasted = false;

            // Trường hợp 1: Thả thẳng lên đầu một Target hợp lệ (Luật 2 & 3)
            if (_currentTarget != null && ValidateTarget(_currentTarget.gameObject))
            {
                // TODO: Truyền _currentTarget sang CombatManager
                isCasted = TryCastCard(_currentTarget.gameObject);
            }
            // Trường hợp 2: Thả giữa không trung cao hơn mốc -> Smart Cast (Luật 1)
            else if (eventData.position.y > smartDropYThreshold)
            {
                // TODO: Chạy logic tự tìm mục tiêu
                isCasted = TrySmartCast();
            }

            // Nếu thất bại (thiếu năng lượng, sai target, thả quá thấp), trôi về tay
            if (!isCasted)
            {
                _rectTransform.anchoredPosition = _originalPosition;
                // Có thể dùng Lerp như bài trước để mượt hơn
            }
        }


        // --- CÁC HÀM KIỂM TRA LOGIC ---
        private bool ValidateTarget(GameObject targetObj)
        {
            CardTargetType type = _cardView.LogicCard.Data.targetType;

            if (type == CardTargetType.SingleEnemy) return targetObj.GetComponent<EnemyController>() != null;
            if (type == CardTargetType.Self) return targetObj.GetComponent<PlayerCharacter>() != null;
            return true;
        }

        private bool TryCastCard(GameObject specificTarget = null)
        {
            // Chúng ta sẽ cần update nhẹ hàm TryPlayCard trong CombatManager để nhận thêm biến target
            return CombatManager.Instance.TryPlayCard(_cardView, specificTarget);
        }

        private bool TrySmartCast()
        {
            return CombatManager.Instance.TryPlayCard(_cardView, null); // Truyền null để CombatManager tự tìm mục tiêu
        }
    }
}