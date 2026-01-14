using UnityEngine;
using UnityEngine.Events;

public class CoinArea : MonoBehaviour
{
    public CoinManager coinManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        coinManager.SetAsCurrentArea();
    }
}
