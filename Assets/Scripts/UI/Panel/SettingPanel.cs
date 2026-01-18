using UnityEngine;
using UnityEngine.UI; // Cần có để dùng Slider

public class SettingPanel : MonoBehaviour
{
  [Header("Music Settings")]
    public Slider musicSlider;
    public Button musicButton;
    public Sprite musicOnSprite;  // Hình cái loa bình thường
    public Sprite musicOffSprite; // Hình cái loa gạch chéo

    private float lastMusicVol = 0.5f;

    private void OnEnable()
    {
        if (AudioManager.HasInstance)
        {
            float savedVol = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            musicSlider.value = savedVol;
            UpdateButtonIcon(savedVol);
        }
    }

    // 1. Gắn vào On Value Changed của Slider
    public void OnMusicSliderChanged(float val)
    {
        AudioManager.Instance.SetMusicVolume(val);
        if (val > 0) lastMusicVol = val;
        UpdateButtonIcon(val);
    }

    // 2. Gắn vào On Click của Button
    public void OnClickToggleMusic()
    {
        if (musicSlider.value > 0) 
        {
            lastMusicVol = musicSlider.value;
            musicSlider.value = 0; // Khi gán = 0, hàm OnMusicSliderChanged sẽ tự chạy
        }
        else 
        {
            musicSlider.value = lastMusicVol;
        }
    }

    // 3. Hàm đổi hình ảnh Icon
    private void UpdateButtonIcon(float val)
    {
        if (val <= 0)
            musicButton.image.sprite = musicOffSprite;
        else
            musicButton.image.sprite = musicOnSprite;
    }

    public void BackToMenu()
    {
        UIManager.Instance.ShowMenu();
    }
}