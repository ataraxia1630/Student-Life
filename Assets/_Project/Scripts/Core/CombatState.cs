namespace DoiSinhVien.Core
{
    public enum CombatState
    {
        Initialize,           // Load data, xáo bài
        Player_Turn_Start,    // Bốc bài, tính buff/debuff 
        Player_Turn_Active,   // Chờ Player đánh bài. Enemy đã chốt Intent hiển thị trên đầu
        Mid_Combat_Event_Check, // [QUAN TRỌNG] Tạm dừng để check Event chen ngang
        Enemy_Turn_Start,     // Xử lý debuff của quái
        Enemy_Turn_Active,    // Quái vật xả skill
        Turn_End_Cleanup,     // Reset Block, xóa bài Exhaust, đưa bài trên tay vào Discard
        Combat_Win,           // Chuyển sang Reward Screen
        Combat_Lose           // Chuyển sang màn hình Burnout 
    }
}