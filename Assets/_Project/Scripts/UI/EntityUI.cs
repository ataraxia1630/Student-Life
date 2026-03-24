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
        public Sprite iconAttack;
        public Sprite iconDefend;

        private void Start()
        {
            if (targetEntity != null)
            {
                _target = targetEntity.GetComponent<ITargetable>();
                _enemy = targetEntity.GetComponent<EnemyController>();

                if (_target != null)
                {
                    hpSlider.maxValue = _target.CurrentHealth; 
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
                    intentText.text = _enemy.CurrentAction.baseValue.ToString();

                    if (_enemy.CurrentAction.intentType == IntentType.Attack)
                        intentIcon.sprite = iconAttack;
                    else if (_enemy.CurrentAction.intentType == IntentType.Block) 
                        intentIcon.sprite = iconDefend;
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