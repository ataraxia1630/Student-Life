using DoiSinhVien.Core;
using UnityEngine;

public class ResetRunData : MonoBehaviour
{
    public void ResetRun()
    {
        PlayerInventory.Instance.ResetRunData();
    }
}
