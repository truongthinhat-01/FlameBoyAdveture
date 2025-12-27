using UnityEngine;

public class EnemyHandDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (UIManager.HasInstance)
            {
                ApplyDamage();
            }
        }
    }

    void ApplyDamage()
    {
        // Trừ máu
        UIManager.Instance.currentHealth -= 1;
        
        if (UIManager.Instance.healthUI != null)
            UIManager.Instance.healthUI.UpdateHealth(UIManager.Instance.currentHealth);

        if (UIManager.Instance.currentHealth <= 0 && UIManagerEvent.HasInstance)
            UIManagerEvent.Instance.LoseGame();
            
        Debug.Log("Tay quái vật đánh trúng Player!");
        
        // Tắt Collider ngay lập tức để tránh đa sát thương trong 1 frame
        // Nó sẽ được EnableAttack() bật lại ở lần vung tay sau
        GetComponent<Collider>().enabled = false;
    }
}