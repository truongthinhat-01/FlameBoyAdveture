using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public void OnClickResume()
    {
        if (UIManagerEvent.HasInstance)
        {
            UIManagerEvent.Instance.ResumeGame();
        }
    }

    public void OnClickRestart()
    {
        if (UIManagerEvent.HasInstance)
        {
            UIManagerEvent.Instance.RestartGame();
        }
    }

    public void OnClickMainMenu()
    {
        if (UIManagerEvent.HasInstance)
        {
            UIManagerEvent.Instance.BackMenu();
        }
    }
}
