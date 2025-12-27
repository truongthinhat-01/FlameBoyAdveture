using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Slider loadingSlider;
    public Text percentText;

    public float fakeSpeed = 0.5f;
    public float pauseAt = 0.9f;
    public float pauseTime = 1.5f;

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad =
            SceneManager.LoadSceneAsync(SceneData.sceneToLoad);

        asyncLoad.allowSceneActivation = false;

        float progress = 0f;

        while (!asyncLoad.isDone)
        {
            float target = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progress = Mathf.MoveTowards(progress, target, fakeSpeed * Time.deltaTime);

            loadingSlider.value = progress;
            percentText.text = Mathf.RoundToInt(progress * 100) + "%";

            if (progress >= pauseAt)
            {
                yield return new WaitForSeconds(pauseTime);
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
