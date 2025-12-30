using UnityEngine;

public class AudioManager : BaseManager<AudioManager>
{
    [SerializeField] private AudioSource musicSource;
    public AudioClip backgroundMusic;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);

        if (musicSource == null) musicSource = GetComponent<AudioSource>();
        
        // Tự động phát nhạc khi vào game
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // --- HÀM CHỈ DÙNG ĐỂ BẬT ---
    public void SetMusicOn()
    {
        if (musicSource != null)
        {
            musicSource.mute = false; // Bỏ chế độ im lặng
            if (!musicSource.isPlaying) 
            {
                musicSource.Play(); // Nếu nhạc đang dừng thì ép phát lại
            }
            Debug.Log("AudioManager: Chỉ thực hiện lệnh BẬT");
        }
    }

    // --- HÀM CHỈ DÙNG ĐỂ TẮT ---
    public void SetMusicOff()
    {
        if (musicSource != null)
        {
            musicSource.mute = true; // Bật chế độ im lặng
            Debug.Log("AudioManager: Chỉ thực hiện lệnh TẮT");
        }
    }
}