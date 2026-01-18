using UnityEngine;

public class SettingTime : MonoBehaviour
{
    public float levelTime = 120f;

    void Start()
    {
        if (CountdownTimer.Instance != null)
        {
            CountdownTimer.Instance.SetTime(levelTime);
        }
    }
}
