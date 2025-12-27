using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : BaseManager<UIManager>
{
    [Header("Panels")]
    public PanelMenu menuPanel;
    public UIShow hudPanel;
    public PausePanel pausePanel;
    public WinPanel winPanel;
    public LosePanel losePanel;
    public SelectMapPanel mapSelectPanel;
    public SettingPanel settingPanel;


    [Header("Loading")]
    public GameObject loadingRoot;
    public LoadingScenePanel loadingBar;

    AsyncOperation asyncLoad;
    string mapToLoad;




    [Header("HUD")]
    public HealthUI healthUI;

    [Header("Player Stats")]
    public int maxHealth = 3;
    public int currentHealth;

    protected override void Awake()
    {
        base.Awake();
        currentHealth = maxHealth;
        DontDestroyOnLoad(this.gameObject);
    }

    void DisableAll()
    {
    if (menuPanel) menuPanel.gameObject.SetActive(false);
    if (mapSelectPanel) mapSelectPanel.gameObject.SetActive(false);
    if (settingPanel) settingPanel.gameObject.SetActive(false);

    if (hudPanel) hudPanel.gameObject.SetActive(false);
    if (pausePanel) pausePanel.gameObject.SetActive(false);
    if (winPanel) winPanel.gameObject.SetActive(false);
    if (losePanel) losePanel.gameObject.SetActive(false);

    if (loadingRoot) loadingRoot.SetActive(false);
    }


    //Menu
    public void ShowMenu()
    {
        DisableAll();
        menuPanel.gameObject.SetActive(true);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    //===== HUD =====
    public void ShowHUD()
    {
        DisableAll();
        hudPanel.gameObject.SetActive(true);

        if (healthUI != null)
        {
            healthUI.Init(maxHealth);
            healthUI.UpdateHealth(currentHealth);
        }

        if (CoinManager.Instance != null)
        {
            hudPanel.UpdateCoinUI(CoinManager.Instance.currentCoin);
        }
    }

    // ===== PAUSE =====
    public void ShowPause(bool show)
    {
        pausePanel.gameObject.SetActive(show);
        Time.timeScale = show ? 0f : 1f;
        AudioListener.pause = show;
    }

    // ===== WIN =====
    public void ShowWin()
    {
        DisableAll();
        winPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    // ===== LOSE =====
    public void ShowLose()
    {
        DisableAll();
        losePanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    // ===== LOAD SCENE =====
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        currentHealth = maxHealth;
        SceneManager.LoadScene(sceneName);
    }
    public void ShowMapSelect()
    {
        DisableAll();

    if (mapSelectPanel != null)
    {
        mapSelectPanel.gameObject.SetActive(true);
                
        if(mapSelectPanel.transform.parent != null)
        {
            mapSelectPanel.transform.parent.gameObject.SetActive(true);
        }
    }
    }

     public void ShowSetting()
    {
        DisableAll();
        if (settingPanel) settingPanel.gameObject.SetActive(true);
    }

     public void ShowLoading()
    {
        DisableAll();
        if (loadingRoot)
            loadingRoot.SetActive(true);
    }

    public void LoadSelectedMap(string mapName)
    {
        ShowLoading();

        asyncLoad = SceneManager.LoadSceneAsync(mapName);
        asyncLoad.allowSceneActivation = false;

        loadingBar.OnLoadingComplete = OnUILoadingDone;
        loadingBar.StartLoading();

        StartCoroutine(SyncLoading());
    }

     IEnumerator SyncLoading()
    {
        while (!asyncLoad.isDone)
        {
            float realProgress = asyncLoad.progress / 0.9f;

            loadingBar.fillImage.fillAmount =
                Mathf.MoveTowards(
                    loadingBar.fillImage.fillAmount,
                    realProgress,
                    loadingBar.fillSpeed * Time.deltaTime
                );

            yield return null;
        }
    }

    void OnUILoadingDone()
    {
        asyncLoad.allowSceneActivation = true;
    }
     public string GetSelectedMap()
    {
        return mapToLoad;
    }

}
