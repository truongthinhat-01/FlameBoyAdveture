using UnityEngine;
using System.Collections;

public class EnemyHandDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra đúng Tag "Player"
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Gọi trực tiếp hàm TakeDamage trên Player
                Debug.Log("Đã đấm trúng Player!");
                
                // Tắt tạm thời và TỰ ĐỘNG bật lại sau 0.8s để có thể đấm phát tiếp theo
                StartCoroutine(ResetHandCollider());
            }
        }
    }

    IEnumerator ResetHandCollider()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false; 
            yield return new WaitForSeconds(0.8f); // Thời gian chờ giữa 2 lần nhận dame
            col.enabled = true;
            Debug.Log("Tay quái vật đã sẵn sàng đánh tiếp");
        }
    }
}