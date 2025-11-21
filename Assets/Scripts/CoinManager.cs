using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [Header("Coin Settings")]
    public int currentCoin = 0;
    public int requiredCoin = 10;

    [Header("Stair")]
    public StairController stair;   // ⬅ kéo script cầu thang vào

    private void Awake()
    {
        Instance = this;
    }

    public void AddCoin(int amount)
    {
        currentCoin += amount;

        if (currentCoin >= requiredCoin)
        {
            stair.OnPlayerComplete();   // ⬅ gọi cầu thang trồi lên
        }
    }
}
