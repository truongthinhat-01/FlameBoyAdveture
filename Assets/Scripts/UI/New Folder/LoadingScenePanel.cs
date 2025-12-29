using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingScenePanel : MonoBehaviour
{
    [Header("UI")]
    public Image fillImage;
    public TMP_Text percentText;

    [Header("Fill Speed")]
    [Tooltip("Tốc độ chạy của thanh loading")]
    public float fillSpeed = 0.4f;

    [Header("Pause Points (0 - 1)")]
    [Tooltip("Các mốc % sẽ khựng (vd: 0.3 = 30%)")]
    public float[] pausePoints;

    [Header("Pause Time")]
    [Tooltip("Thời gian khựng tương ứng với từng mốc")]
    public float[] pauseTimes;

    public Action OnLoadingComplete;

    Coroutine routine;

    public void StartLoading()
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(LoadingRoutine());
    }

    // IEnumerator LoadingRoutine()
    // {
    //     fillImage.fillAmount = 0f;

    //     bool[] paused = new bool[pausePoints.Length];

    //     while (fillImage.fillAmount < 1f)
    //     {
    //         fillImage.fillAmount += Time.deltaTime * fillSpeed;
    //         fillImage.fillAmount = Mathf.Clamp01(fillImage.fillAmount);

    //         // Update % text
    //         if (percentText)
    //             percentText.text = Mathf.RoundToInt(fillImage.fillAmount * 100f) + "%";

    //         // Check pause points
    //         for (int i = 0; i < pausePoints.Length; i++)
    //         {
    //             if (!paused[i] && fillImage.fillAmount >= pausePoints[i])
    //             {
    //                 paused[i] = true;
    //                 yield return new WaitForSeconds(pauseTimes[i]);
    //             }
    //         }

    //         yield return null;
    //     }

    //     fillImage.fillAmount = 1f;

    //     if (percentText)
    //         percentText.text = "100%";

    //     OnLoadingComplete?.Invoke();
    // }

    // Trong LoadingScenePanel.cs

IEnumerator LoadingRoutine()
{
    fillImage.fillAmount = 0f;
    bool[] paused = new bool[pausePoints.Length];

    while (fillImage.fillAmount < 1f)
    {
        // Tăng thanh bar mượt mà theo fillSpeed
        fillImage.fillAmount += Time.deltaTime * fillSpeed;
        fillImage.fillAmount = Mathf.Clamp01(fillImage.fillAmount);

        if (percentText != null)
            percentText.text = Mathf.RoundToInt(fillImage.fillAmount * 100f) + "%";

        // Xử lý các điểm dừng (nếu có)
        for (int i = 0; i < pausePoints.Length; i++)
        {
            if (!paused[i] && fillImage.fillAmount >= pausePoints[i])
            {
                paused[i] = true;
                yield return new WaitForSeconds(pauseTimes[i]);
            }
        }
        yield return null;
    }

    // Đảm bảo đạt 100% trước khi kết thúc
    fillImage.fillAmount = 1f;
    if (percentText != null) percentText.text = "100%";

    yield return new WaitForSeconds(0.2f); // Chờ ngắn để người chơi thấy 100%

    // QUAN TRỌNG: Gọi sự kiện để UIManager kích hoạt allowSceneActivation
    OnLoadingComplete?.Invoke();
}
}
