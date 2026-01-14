using UnityEngine;

public class Coin : MonoBehaviour
{
    CoinManager manager;

    void Awake()
    {
        manager = GetComponentInParent<CoinManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        manager?.AddCoin();
        gameObject.SetActive(false);
    }
}
