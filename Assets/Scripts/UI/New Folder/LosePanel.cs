using UnityEngine;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
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
