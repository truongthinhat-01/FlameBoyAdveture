using UnityEngine;

public class TriggerHeartHeal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();
        if (player != null)
        {
            player.HealFull();
            Destroy(gameObject); // ăn xong là mất
        }
    }
}
