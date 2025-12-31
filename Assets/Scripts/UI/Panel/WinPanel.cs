using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    // ===== NEXT LEVEL =====
    public void OnClickNextLevel()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    // ===== RESTART =====
    public void OnClickRestart()
    {
        if (UIManagerEvent.HasInstance)
        {
            UIManagerEvent.Instance.RestartGame();
        }
    }

    // ===== MAIN MENU =====
    public void OnClickMainMenu()
    {
        if (UIManagerEvent.HasInstance)
        {
            UIManagerEvent.Instance.BackMenu();
        }
    }
}
