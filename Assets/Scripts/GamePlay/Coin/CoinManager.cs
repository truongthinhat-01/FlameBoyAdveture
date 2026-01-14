// using UnityEngine;

// public class CoinManager : MonoBehaviour
// {
//     public static CoinManager Instance;

//     [Header("Coin Settings")]
//     public int requiredCoin = 10;
//     public int currentCoin;

//     [Header("Target Object")]
//     public GameObject gameObj; // cửa / cầu thang / boss ...

//     void Awake()
//     {
//         if (Instance == null)
//             Instance = this;
//         else
//         {
//             Destroy(gameObject);
//             return;
//         }
//     }

//     void Start()
//     {
//         currentCoin = 0;

//         if (gameObj != null)
//             gameObj.SetActive(false);

//         UpdateCoinUI();
//     }

//     // ===== ADD COIN =====
//     public void AddCoin(int amount)
//     {
//         currentCoin += amount;
//         UpdateCoinUI();

//         if (currentCoin >= requiredCoin)
//         {
//             Unlock();
//         }
//     }

//     // ===== UPDATE UI =====
//     void UpdateCoinUI()
//     {
//         if (UIManager.HasInstance &&
//             UIManager.Instance.hudPanel.gameObject.activeSelf)
//         {
//             UIManager.Instance.hudPanel.UpdateCoinUI(currentCoin);
//         }
//     }

//     // ===== UNLOCK =====
//     void Unlock()
//     {
//         Debug.Log("ĐỦ COIN!");

//         if (gameObj != null)
//             gameObj.SetActive(true);
//     }
// }


using UnityEngine;

public class CoinManager : MonoBehaviour
{
    
    public static CoinManager Instance;
 

    [Header("Coin")]
    public int requiredCoin = 4;
    public int currentCoin;

    [Header("Target")]
    public GameObject gameObj; // cửa / boss / portal

    bool unlocked;

    void Start()
    {
        currentCoin = 0;
        unlocked = false;

        if (gameObj != null)
            gameObj.SetActive(false);

        UpdateUI();
    }

    public void AddCoin(int amount = 1)
    {
        if (unlocked) return;

        currentCoin += amount;
        currentCoin = Mathf.Min(currentCoin, requiredCoin);

        UpdateUI();

        if (currentCoin >= requiredCoin)
        {
            Unlock();
        }
    }

    void UpdateUI()
    {
        if (UIManager.HasInstance &&
            UIManager.Instance.hudPanel.gameObject.activeSelf)
        {
            // 👉 GIỮ NGUYÊN CÁCH GỌI UI CỦA BẠN
            UIManager.Instance.hudPanel.UpdateCoinUI(currentCoin);
        }
    }

    void Unlock()
    {
        unlocked = true;

        if (gameObj != null)
            gameObj.SetActive(true);

        Debug.Log("ĐỦ COIN KHU NÀY");
    }
    public void SetAsCurrentArea()
    {
          Instance = this;
    if (UIManager.HasInstance &&
        UIManager.Instance.hudPanel.gameObject.activeSelf)
    {
        // đẩy UI về 0/x hoặc current/x
        UIManager.Instance.hudPanel.UpdateCoinUI(currentCoin);
    }
    }

}
