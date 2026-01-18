using UnityEngine;

public class Map2Coin : MonoBehaviour
{
    public Map2BossManager bossManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        bossManager.AddCoin(1);
        gameObject.SetActive(false);
    }
}
