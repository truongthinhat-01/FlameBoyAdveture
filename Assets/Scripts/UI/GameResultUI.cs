using UnityEngine;
using UnityEngine.Events;

public class GameResultUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public UnityEvent OnRestart;
    public UnityEvent OnExit;

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Restart()
    {
        OnRestart?.Invoke();
    }

    public void Exit()
    {
        OnExit?.Invoke();
    }
}
