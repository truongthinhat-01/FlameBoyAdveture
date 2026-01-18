using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    public CoinManager areaCoinManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            areaCoinManager.SetAsCurrentArea();
        }
    }
}
