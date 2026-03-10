namespace DoiSinhVien.Core
{
    public interface ITargetable
    {
        void TakeDamage(int amount);
        void GainBlock(int amount);
    }
}