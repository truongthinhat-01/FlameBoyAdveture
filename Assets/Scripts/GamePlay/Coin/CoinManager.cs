using UnityEngine;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    

    [Header("Coin")]
    public int requiredCoin = 4;
    public int currentCoin;
    public bool unlocked;

    // [Header("Boss")]
    // public GameObject boss;   // 👈 BOSS CỦA KHU NÀY

     [Header("Enemies To Spawn")]
    public List<GameObject> enemies;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // public void SetAsCurrentArea()
    // {
    //     Instance = this;
    //     UpdateUI();
    // }

    public void SetAsCurrentArea()
{
    Instance = this;
    ResetCoin(requiredCoin); // 🔥 RESET KHI QUA KHU
}


    public void ResetCoin(int newRequiredCoin)
    {
        requiredCoin = newRequiredCoin;
        currentCoin = 0;
        unlocked = false;

        // if (boss != null)
        //     boss.SetActive(false); // 👈 chưa đủ coin thì boss ẩn
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
                enemy.SetActive(false);
        }
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
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        // unlocked = true;

        // if (boss != null)
        // {
        //     boss.SetActive(true);   // 👈 BOSS XUẤT HIỆN
        //     Debug.Log("👹 BOSS XUẤT HIỆN!");
        // }

         unlocked = true;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;
            
            enemy.SetActive(true);
           

            ISpawnable spawnable = enemy.GetComponent<ISpawnable>();
            if (spawnable != null)
            {
                spawnable.Spawn(); // 🔥 gọi spawn chuẩn
            }
        }

        Debug.Log("👹 ENEMIES ĐÃ ĐƯỢC SPAWN!");
    }

    void UpdateUI()
    {
        if (UIManager.HasInstance)
            UIManager.Instance.hudPanel.UpdateCoinUI(currentCoin);
    }
}
