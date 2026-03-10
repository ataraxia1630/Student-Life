namespace DoiSinhVien.Core
{
    public interface ITargetable
    {
        int CurrentHealth { get; }
        void TakeDamage(int amount);
        void GainBlock(int amount);
        void SetHealth(int amount);
    }
}