using UnityEngine;

public class PanelMenu : MonoBehaviour
{
    // Gọi khi nhấn nút "New Game" hoặc "Select Map"
    public void NewGame()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowMapSelect();
        }
    }

    // Gọi khi nhấn nút "Settings"
    public void Setting()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowSetting();
        }
    }

    // Gọi khi nhấn nút "Exit"
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}