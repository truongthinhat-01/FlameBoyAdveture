using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : BaseManager<AudioManager>
{
    public AudioMixer mainMixer;
    private bool isMuted = false;

    void Start()
    {
        // Khi game bắt đầu, lấy giá trị đã lưu để áp dụng cho Mixer
        // Nếu chưa có dữ liệu, mặc định sẽ là 0.75f
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        // Áp dụng ngay lập tức
        SetMusicVolume(savedMusic);
        SetSFXVolume(savedSFX);
    }

    public void SetMusicVolume(float value)
    {
        if (isMuted && value > 0) isMuted = false; // Tự động bỏ mute nếu kéo slider
        
        float db = value <= 0.0001f ? -80f : Mathf.Log10(value) * 20;
        mainMixer.SetFloat("MusicVol", db);
        
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save(); // Lưu vào bộ nhớ máy
    }

    public void SetSFXVolume(float value)
    {
        if (isMuted && value > 0) isMuted = false;
        
        float db = value <= 0.0001f ? -80f : Mathf.Log10(value) * 20;
        mainMixer.SetFloat("SFXVol", db);
        
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }

    public void ToggleMuteAll()
    {
        isMuted = !isMuted;
        
        if (isMuted)
        {
            mainMixer.SetFloat("MusicVol", -80f);
            mainMixer.SetFloat("SFXVol", -80f);
        }
        else
        {
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.75f));
            SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0.75f));
        }
    }
}