using UnityEngine;
using System.Collections.Generic;

namespace DoiSinhVien.Core
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.1f, 3f)] public float pitch = 1f;
        public bool loop = false;
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Audio Sources")]
        [Tooltip("Nguồn phát nhạc nền")]
        public AudioSource bgmSource;
        [Tooltip("Nguồn phát hiệu ứng âm thanh (có thể tạo nhiều cái nếu cần phát đè nhau)")]
        public AudioSource sfxSource;

        [Header("Audio Database")]
        public Sound[] bgmSounds;
        public Sound[] sfxSounds;

        private Dictionary<string, Sound> bgmDictionary = new Dictionary<string, Sound>();
        private Dictionary<string, Sound> sfxDictionary = new Dictionary<string, Sound>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            foreach (Sound s in bgmSounds)
            {
                if (!bgmDictionary.ContainsKey(s.name)) bgmDictionary.Add(s.name, s);
            }

            foreach (Sound s in sfxSounds)
            {
                if (!sfxDictionary.ContainsKey(s.name)) sfxDictionary.Add(s.name, s);
            }
        }


        public void PlayBGM(string soundName)
        {
            if (bgmDictionary.TryGetValue(soundName, out Sound sound))
            {
                if (bgmSource.clip == sound.clip && bgmSource.isPlaying) return;

                bgmSource.clip = sound.clip;
                bgmSource.volume = sound.volume;
                bgmSource.pitch = sound.pitch;
                bgmSource.loop = sound.loop; 
                bgmSource.Play();
            }
            else
            {
                Debug.LogWarning($"[AudioManager] Không tìm thấy BGM mang tên: {soundName}");
            }
        }

        public void StopBGM()
        {
            bgmSource.Stop();
        }

        public void PlaySFX(string soundName)
        {
            if (sfxDictionary.TryGetValue(soundName, out Sound sound))
            {
                sfxSource.pitch = sound.pitch;
                sfxSource.PlayOneShot(sound.clip, sound.volume);
            }
            else
            {
                Debug.LogWarning($"[AudioManager] Không tìm thấy SFX mang tên: {soundName}");
            }
        }

        public void PlaySFXRandomPitch(string soundName, float minPitch = 0.9f, float maxPitch = 1.1f)
        {
            if (sfxDictionary.TryGetValue(soundName, out Sound sound))
            {
                sfxSource.pitch = Random.Range(minPitch, maxPitch);
                sfxSource.PlayOneShot(sound.clip, sound.volume);

                sfxSource.pitch = 1f;
            }
            else
            {
                Debug.LogWarning($"[AudioManager] Không tìm thấy SFX mang tên: {soundName}");
            }
        }
    }
}