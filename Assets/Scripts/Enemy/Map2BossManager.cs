using UnityEngine;

public class Map2BossManager : MonoBehaviour
{
public static Map2BossManager Instance;

    [Header("Coin")]
    public int requiredCoin = 5;
    public int currentCoin;

    [Header("Skeleton")]
    public SkeletonController skeleton;

    bool spawned;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentCoin = 0;
        spawned = false;

        if (skeleton != null)
            skeleton.gameObject.SetActive(false);

        UpdateUI();
    }

    public void AddCoin(int amount = 1)
    {
        if (spawned) return;

        currentCoin += amount;
        currentCoin = Mathf.Min(currentCoin, requiredCoin);

        Debug.Log($"🪙 SKE COIN {currentCoin}/{requiredCoin}");

        UpdateUI();

        if (currentCoin >= requiredCoin)
        {
            SpawnSkeleton();
        }
    }

    void SpawnSkeleton()
    {
        spawned = true;

        if (skeleton != null)
        {
            skeleton.Spawn();
            Debug.Log("💀 SKELETON SPAWN");
        }
    }

    void UpdateUI()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.hudPanel.UpdateCoinUI(currentCoin);
        }
    }
}
