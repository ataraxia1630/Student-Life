using UnityEngine;
using TMPro;
using DG.Tweening;
using DoiSinhVien.Core;

namespace DoiSinhVien.Visual
{
    public class VFXManager : MonoBehaviour
    {
        public static VFXManager Instance { get; private set; }

        [Header("Floating Text Prefabs")]
        public GameObject damageTextPrefab; 
        public GameObject blockTextPrefab;  

        [Header("Skill VFX Prefabs")]
        public GameObject slashVFX;  
        public GameObject shieldVFX;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void OnEnable()
        {
            GameEvents.OnEntityDamaged += ShowDamageText;
            GameEvents.OnEntityGainedBlock += ShowBlockText;
            GameEvents.OnPlayVFX += PlaySkillVFX;
        }

        private void OnDisable()
        {
            GameEvents.OnEntityDamaged -= ShowDamageText;
            GameEvents.OnEntityGainedBlock -= ShowBlockText;
            GameEvents.OnPlayVFX -= PlaySkillVFX;
        }

        private void ShowDamageText(Transform target, int amount)
        {
            if (amount <= 0 || target == null) return;
            SpawnFloatingText(damageTextPrefab, target.position, $"-{amount}", Color.red);
        }

        private void ShowBlockText(Transform target, int amount)
        {
            if (amount <= 0 || target == null) return;
            SpawnFloatingText(blockTextPrefab, target.position, $"+{amount}", new Color(0.2f, 0.6f, 1f)); // Màu xanh dương nhạt
        }

        private void SpawnFloatingText(GameObject prefab, Vector3 pos, string text, Color color)
        {
            if (prefab == null) return;

            Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0f, 0.5f), 0);
            Vector3 spawnPos = pos + randomOffset + Vector3.up; 

            GameObject go = Instantiate(prefab, spawnPos, Quaternion.identity);
            TMP_Text tmp = go.GetComponentInChildren<TMP_Text>();

            if (tmp != null)
            {
                tmp.text = text;
                tmp.color = color;

                go.transform.localScale = Vector3.zero;
                Sequence seq = DOTween.Sequence();

                seq.Append(go.transform.DOScale(1.5f, 0.2f).SetEase(Ease.OutBack));
                seq.Append(go.transform.DOScale(1f, 0.1f));

                seq.Join(go.transform.DOMoveY(spawnPos.y + 1.5f, 0.8f).SetEase(Ease.OutCubic));

                seq.Join(tmp.DOFade(0, 0.5f).SetDelay(0.3f));

                seq.OnComplete(() => Destroy(go));
            }
        }

        private void PlaySkillVFX(Transform target, VFXType type)
        {
            if (target == null) return;
            GameObject vfxPrefab = null;

            switch (type)
            {
                case VFXType.Slash: vfxPrefab = slashVFX; break;
                case VFXType.Shield: vfxPrefab = shieldVFX; break;
            }

            if (vfxPrefab != null)
            {
                GameObject vfx = Instantiate(vfxPrefab, target.position, Quaternion.identity);
                Destroy(vfx, 1.5f);
            }
        }
    }
}