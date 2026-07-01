using UnityEngine;

namespace DoiSinhVien.Visual
{
    public class BrealthingAnimation : MonoBehaviour
    {
        [Header("Breathing Settings")]
        [Tooltip("Tốc độ hô hấp (nhịp thở). Số càng lớn thở càng gấp.")]
        public float breathSpeed = 3f;

        [Tooltip("Biên độ phình ra / xẹp xuống.")]
        public float breathMagnitude = 0.05f;

        [Tooltip("Kích hoạt Squash & Stretch: Khi Y phình to thì X bóp nhỏ lại, tạo cảm giác mập mạp, co giãn tự nhiên.")]
        public bool useSquashAndStretch = true;

        private Vector3 originalScale;
        private float randomOffset;

        private void Start()
        {
            originalScale = transform.localScale;

            randomOffset = Random.Range(0f, 2f * Mathf.PI);
        }

        private void Update()
        {
            float breath = Mathf.Sin(Time.time * breathSpeed + randomOffset) * breathMagnitude;

            if (useSquashAndStretch)
            {
                transform.localScale = new Vector3(
                    originalScale.x - (breath * 0.5f),
                    originalScale.y + breath,
                    originalScale.z
                );
            }
            else
            {
                transform.localScale = new Vector3(
                    originalScale.x,
                    originalScale.y + breath,
                    originalScale.z
                );
            }
        }

        public void StopBreathing()
        {
            this.enabled = false;
            transform.localScale = originalScale;
        }
    }
}