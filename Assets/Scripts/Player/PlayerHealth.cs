using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public Animator anim;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowHUD();

            if (UIManager.Instance.healthUI != null)
            {
                UIManager.Instance.healthUI.Init(maxHealth);
                UIManager.Instance.healthUI.UpdateHealth(currentHealth);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (UIManager.HasInstance && UIManager.Instance.healthUI != null)
            UIManager.Instance.healthUI.UpdateHealth(currentHealth);

        // 👉 KHÔNG animation khi chưa chết

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        // 👉 CHỈ TỚI ĐÂY MỚI CHẠY DEATH
        if (anim != null)
            anim.SetBool("isDead", true);

        // 👉 KHÓA PLAYER
        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null) pc.enabled = false;

        // 👉 TẮT COLLIDER
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        // 👉 DỪNG DI CHUYỂN
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // 👉 ĐỢI ANIM CHẾT CHẠY XONG RỒI MỚI LOSE
        Invoke(nameof(ShowLose), 2.5f); // chỉnh theo độ dài anim
    }

    void ShowLose()
    {
        if (UIManager.HasInstance)
            UIManager.Instance.ShowLose();
    }
}
