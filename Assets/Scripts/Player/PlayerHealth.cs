using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    public int currentHealth;
    
    [Header("References")]
    public Animator anim;
    
    private bool isDead = false;
    private bool isInvulnerable = false;

    void Start()
    {
        currentHealth = maxHealth;
        
        // Khởi tạo UI ban đầu
        if (UIManager.HasInstance && UIManager.Instance.healthUI != null)
        {
            UIManager.Instance.currentHealth = currentHealth;
            UIManager.Instance.healthUI.Init(maxHealth);
            UIManager.Instance.healthUI.UpdateHealth(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvulnerable) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Cập nhật trái tim trên UI
        if (UIManager.HasInstance)
        {
            UIManager.Instance.UpdatePlayerHealth(currentHealth);
        }

        if (currentHealth <= 0) 
        {
            StartCoroutine(DieRoutine()); // Gọi Coroutine xử lý cái chết
        }
        else 
        {
            StartCoroutine(InvulnerableRoutine());
        }
    }

    IEnumerator InvulnerableRoutine()
    {
        isInvulnerable = true; 
        if (anim != null) anim.SetTrigger("hit");
        yield return new WaitForSeconds(1.0f);
        isInvulnerable = false;
    }

    // Logic xử lý cái chết để Animation kịp chạy
    IEnumerator DieRoutine()
    {
        if (isDead) yield break;
        isDead = true;

        Debug.Log("Player bắt đầu diễn Animation chết...");

        // 1. Kích hoạt Animation chết
        if (anim != null) 
        {
            anim.SetBool("isDead", true); 
        }
        
        // 2. Gọi hàm Die bên PlayerController để khóa di chuyển và Input
        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null) pc.Die();

        // 3. CHỜ ĐỢI: Đây là phần quan trọng nhất. 
        // Phải đợi cho nhân vật ngã xuống xong rồi mới hiện bảng Lose.
        // Dùng yield return new WaitForSeconds để đợi theo thời gian thực của game.
        yield return new WaitForSeconds(2.5f); 

        // 4. Sau khi đợi xong, mới hiện bảng Lose (Lúc này Time.timeScale mới về 0)
        if (UIManager.HasInstance) 
        {
            UIManager.Instance.ShowLose();
        }
    }
}