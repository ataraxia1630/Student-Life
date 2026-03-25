using DoiSinhVien.Data;
using UnityEngine;
using UnityEngine.UI;

namespace DoiSinhVien.Core
{
    public class RoomNode : MonoBehaviour
    {
        [Header("Setup")]
        [Tooltip("Kéo Blueprint bạn muốn test vào đây")]
        public EncounterBlueprint roomBlueprint;

        public void OnRoomSelected()
        {
            Debug.Log($"Đã chọn phòng! Chuẩn bị test Blueprint: {roomBlueprint.blueprintName}");

            BattleManager.Instance.StartBattle(roomBlueprint);
        }
    }
}