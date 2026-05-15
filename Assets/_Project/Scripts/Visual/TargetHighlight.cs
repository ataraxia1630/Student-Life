using UnityEngine;

namespace DoiSinhVien.Visual
{
    public class TargetHighlight : MonoBehaviour
    {
        [Header("Highlight Settings")]
        public SpriteRenderer spriteRenderer; 
        public Color normalColor = Color.white;
        public Color validHighlightColor = Color.red; 
        public float scaleMultiplier = 1.1f;  

        private Vector3 _originalScale;

        private void Awake()
        {
            if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _originalScale = transform.localScale;
            Unhighlight();
        }

        public void HighlightAsValid()
        {
            spriteRenderer.color = validHighlightColor;
            transform.localScale = _originalScale * scaleMultiplier;
        }

        public void Unhighlight()
        {
            spriteRenderer.color = normalColor;
            transform.localScale = _originalScale;
        }
    }
}