using DoiSinhVien.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DoiSinhVien.Core
{
    public class RoomNode : MonoBehaviour
    {
        [Header("Setup")]
        public EncounterBlueprint roomBlueprint;

        [Tooltip("Tên chính xác của Scene Combat trong Project")]
        public string combatSceneName = "Combat_Prototype";

        public void OnRoomSelected()
        {
            if (RunManager.Instance != null)
            {
                RunManager.Instance.pendingEncounter = roomBlueprint;
            }
            else
            {
                Debug.LogError("Không tìm thấy RunManager! Hãy đảm bảo đã tạo GameObject RunManager ở Scene đầu tiên.");
                return;
            }

            SceneManager.LoadScene(combatSceneName);
        }
    }
}