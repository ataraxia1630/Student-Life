using DoiSinhVien.Core;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    void Start()
    {
        coinText.text = "Coin: " + PlayerInventory.Instance.currentCredits.ToString() + "k";
    }

    void Update()
    {
        coinText.text = "Coin: " + PlayerInventory.Instance.currentCredits.ToString() + "k";
    }
}
