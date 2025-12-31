using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    // Gắn cái này vào nút Bật (Button ON)
    public void OnClickTurnOn()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.SetMusicOn();
        }
    }

    // Gắn cái này vào nút Tắt (Button OFF)
    public void OnClickTurnOff()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.SetMusicOff();
        }
    }

    public void BackToMenu()
    {
        UIManager.Instance.ShowMenu();
    }
}