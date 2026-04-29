using UnityEngine;

namespace DoiSinhVien.Data
{
    public interface IPurchasable
    {
        string ItemName { get; }
        int ItemPrice { get; }

        void OnPurchased();
    }
}