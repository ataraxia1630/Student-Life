using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering; 

namespace DoiSinhVien.UI
{
    public class CardHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("References")]
        public RectTransform visualTransform;

        [Tooltip("Kéo cục chứa Sorting Group vào đây")]
        public SortingGroup sortingGroup;

        [Header("Hover Settings")]
        public float hoverScale = 1.3f;
        public float hoverPushUp = 2f;
        public float smoothSpeed = 15f;

        [Header("Sorting Settings")]
        public int normalSortOrder = 0;
        public int hoverSortOrder = 100; 

        private Vector3 _originalScale;
        private Vector2 _originalLocalPos;

        private Vector3 _targetScale;
        private Vector2 _targetLocalPos;

        private void Awake()
        {
            _originalScale = visualTransform.localScale;
            _originalLocalPos = visualTransform.localPosition;

            _targetScale = _originalScale;
            _targetLocalPos = _originalLocalPos;

            if (sortingGroup == null) sortingGroup = GetComponentInChildren<SortingGroup>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (sortingGroup != null)
            {
                sortingGroup.sortingOrder = hoverSortOrder;
            }

            _targetScale = _originalScale * hoverScale;
            _targetLocalPos = _originalLocalPos + new Vector2(0, hoverPushUp);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (sortingGroup != null)
            {
                sortingGroup.sortingOrder = normalSortOrder;
            }

            _targetScale = _originalScale;
            _targetLocalPos = _originalLocalPos;
        }

        private void Update()
        {
            visualTransform.localScale = Vector3.Lerp(visualTransform.localScale, _targetScale, Time.deltaTime * smoothSpeed);
            visualTransform.localPosition = Vector2.Lerp(visualTransform.localPosition, _targetLocalPos, Time.deltaTime * smoothSpeed);
        }
    }
}