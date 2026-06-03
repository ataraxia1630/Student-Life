using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DoiSinhVien.Core;

namespace DoiSinhVien.UI
{
    public class EntityUI : MonoBehaviour
    {
        [Header("Target Data")]
        public GameObject targetEntity; 
        private ITargetable _target;
        private EnemyController _enemy; 

        [Header("Health & Block")]
        public Slider hpSlider;
        public Image fillImage; 
        public TextMeshProUGUI hpText;
        public GameObject blockGroup;
        public TextMeshProUGUI blockText;

        [Header("Enemy Intent")]
        public GameObject intentGroup;
        public Image intentIcon;
        public TextMeshProUGUI intentText;

        private void Start()
        {
            if (targetEntity != null)
            {
                _target = targetEntity.GetComponent<ITargetable>();
                _enemy = targetEntity.GetComponent<EnemyController>();

                if (_target != null)
                {
                    if (_target is PlayerCharacter) hpSlider.maxValue = PlayerInventory.Instance.maxHealth;
                    else hpSlider.maxValue = _target.CurrentHealth; 
                }
            }
        }

        private void Update()
        {
            if (_target == null) return;

            hpSlider.value = _target.CurrentHealth;
            hpText.text = $"{_target.CurrentHealth}/{hpSlider.maxValue}";

            if (_target.CurrentBlock > 0)
            {
                blockGroup.SetActive(true);
                blockText.text = _target.CurrentBlock.ToString();
            }
            else
            {
                blockGroup.SetActive(false);
            }

            if (_enemy != null && intentGroup != null)
            {
                if (_enemy.CurrentAction != null)
                {
                    intentGroup.SetActive(true);
                    intentText.text = _enemy.CurrentIntentValue.ToString();

                    if (ThemeManager.Instance != null && ThemeManager.Instance.combatVisuals != null)
                    {
                        var config = ThemeManager.Instance.combatVisuals.GetIntentVisual(_enemy.CurrentAction.intentType);
                        if (config.icon != null) intentIcon.sprite = config.icon;
                        intentIcon.color = config.iconColor;
                    }
                }
                else
                {
                    intentGroup.SetActive(false); 
                }
            }
            else if (intentGroup != null)
            {
                intentGroup.SetActive(false); 
            }
        }
    }
}