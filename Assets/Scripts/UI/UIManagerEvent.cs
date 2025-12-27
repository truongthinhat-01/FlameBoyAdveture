using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerEvent : BaseManager<UIManagerEvent>
{
    bool isPaused;
    [SerializeField] string gameplayScene = "PlayGame";

    protected override void Awake()
{
    base.Awake();

    SceneManager.sceneLoaded += OnSceneLoaded;
}
private void OnDestroy()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;

}
void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if (!UIManager.HasInstance) return;

    if (scene.name == UIManager.Instance.GetSelectedMap())
    {
        UIManager.Instance.ShowHUD();
    }
}


    // ===== PAUSE =====
    public void PauseGame()
    {
        
        Time.timeScale = 0f;
        UIManager.Instance.ShowPause(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        UIManager.Instance.ShowPause(false);
    }

    // ===== WIN / LOSE =====
    public void WinGame()
    {
        isPaused = false;
        UIManager.Instance.ShowWin();
    }

    public void LoseGame()
    {
        isPaused = false;
        UIManager.Instance.ShowLose();
    }

    // ===== RESTART =====
    public void RestartGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    // ===== BACK MENU =====
    public void BackMenu()
{
    isPaused = false;
    Time.timeScale = 1f;
    AudioListener.pause = false;

    UIManager.Instance.ShowMenu(); // ✅ BẬT PANEL MENU
}

    // ===== QUIT =====
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

