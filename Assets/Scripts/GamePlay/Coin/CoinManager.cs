using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [Header("Coin Settings")]
    public int requiredCoin = 10;
    public int currentCoin;

    [Header("Target Object")]
    public GameObject gameObj; // cửa / cầu thang / boss ...

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        currentCoin = 0;

        if (gameObj != null)
            gameObj.SetActive(false);

        UpdateCoinUI();
    }

    // ===== ADD COIN =====
    public void AddCoin(int amount)
    {
        currentCoin += amount;
        UpdateCoinUI();

        if (currentCoin >= requiredCoin)
        {
            Unlock();
        }
    }

    // ===== UPDATE UI =====
    void UpdateCoinUI()
    {
        if (UIManager.HasInstance &&
            UIManager.Instance.hudPanel.gameObject.activeSelf)
        {
            UIManager.Instance.hudPanel.UpdateCoinUI(currentCoin);
        }
    }

    // ===== UNLOCK =====
    void Unlock()
    {
        Debug.Log("ĐỦ COIN!");

        if (gameObj != null)
            gameObj.SetActive(true);
    }
}
