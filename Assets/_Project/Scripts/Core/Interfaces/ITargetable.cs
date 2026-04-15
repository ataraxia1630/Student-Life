namespace DoiSinhVien.Core
{
    public interface ITargetable
    {
        int CurrentHealth { get; }
        int CurrentBlock { get; }
        void TakeDamage(int amount);
        void GainBlock(int amount);
        void SetHealth(int amount);
        void ResetBlock();
    }
}