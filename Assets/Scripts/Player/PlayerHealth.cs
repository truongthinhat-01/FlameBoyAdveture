// // using UnityEngine;
// // using System.Collections;

// // public class PlayerHealth : MonoBehaviour
// // {
// //     public int maxHealth = 3;
// //     public int currentHealth;
// //     public Animator anim;
    
// //     private bool isDead = false;
// //     private bool isInvulnerable = false;

// //     void Start()
// //     {
// //         currentHealth = maxHealth;
        
// //         // Khởi tạo thanh tim ngay khi vào Map
// //         if (UIManager.HasInstance && UIManager.Instance.healthUI != null)
// //         {
// //             UIManager.Instance.currentHealth = currentHealth;
// //             UIManager.Instance.healthUI.Init(maxHealth); // Tạo 3 tim
// //             UIManager.Instance.healthUI.UpdateHealth(currentHealth); // Hiển thị đúng số tim
// //         }
// //     }

// //     public void TakeDamage(int damage)
// //     {
// //         if (isDead || isInvulnerable) return;

// //         currentHealth -= damage;
// //         currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

// //         // Báo cho UIManager để cập nhật UI trái tim
// //         if (UIManager.HasInstance)
// //         {
// //             UIManager.Instance.UpdatePlayerHealth(currentHealth);
// //         }

// //         if (currentHealth <= 0) Die();
// //         else StartCoroutine(InvulnerableRoutine());
// //     }

// //     IEnumerator InvulnerableRoutine()
// //     {
// //         isInvulnerable = true; 
// //         if (anim != null) anim.SetTrigger("hit"); // Chạy anim bị đau (nếu có)
// //         yield return new WaitForSeconds(1.0f); // Bất tử 1 giây
// //         isInvulnerable = false;
// //     }

// //     void Die()
// //     {
// //         if (isDead) return;
// //         isDead = true;

// //         if (anim != null) anim.SetBool("isDead", true);
        
// //         // Gọi hàm Die bên PlayerController của bạn để khóa di chuyển
// //         PlayerController pc = GetComponent<PlayerController>();
// //         if (pc != null) pc.Die();

// //         Invoke(nameof(ShowLosePanel), 2.5f);
// //     }

// //     void ShowLosePanel() {
// //         if (UIManager.HasInstance) UIManager.Instance.ShowLose();
// //     }
// // }

// using UnityEngine;
// using System.Collections;

// public class PlayerHealth : MonoBehaviour
// {
//     public int maxHealth = 3;
//     public int currentHealth;
//     public Animator anim;
    
//     private bool isDead = false;
//     private bool isInvulnerable = false;

//     void Start()
//     {
//         currentHealth = maxHealth;
        
//         if (UIManager.HasInstance && UIManager.Instance.healthUI != null)
//         {
//             UIManager.Instance.currentHealth = currentHealth;
//             UIManager.Instance.healthUI.Init(maxHealth);
//             UIManager.Instance.healthUI.UpdateHealth(currentHealth);
//         }
//     }

//     public void TakeDamage(int damage)
//     {
//         if (isDead || isInvulnerable) return;

//         currentHealth -= damage;
//         currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

//         if (UIManager.HasInstance)
//         {
//             UIManager.Instance.UpdatePlayerHealth(currentHealth);
//         }

//         if (currentHealth <= 0) 
//         {
//             StartCoroutine(DieRoutine()); // Chuyển sang dùng Coroutine cho cái chết
//         }
//         else 
//         {
//             StartCoroutine(InvulnerableRoutine());
//         }
//     }

//     IEnumerator InvulnerableRoutine()
//     {
//         isInvulnerable = true; 
//         if (anim != null) anim.SetTrigger("hit");
//         yield return new WaitForSeconds(1.0f);
//         isInvulnerable = false;
//     }

//     // Coroutine xử lý cái chết để Animation kịp chạy
//     IEnumerator DieRoutine()
//     {
//         if (isDead) yield break;
//         isDead = true;

//         // 1. Kích hoạt Animation chết
//         if (anim != null) 
//         {
//             anim.SetBool("isDead", true);
//             // Ép Animator cập nhật ngay lập tức nếu cần
//         }
        
//         // 2. Khóa điều khiển nhân vật
//         PlayerController pc = GetComponent<PlayerController>();
//         if (pc != null) pc.Die();

//         // 3. CHỜ ĐỢI: Đợi 2.5 giây cho nhân vật ngã xuống xong xuôi
//         // Lưu ý: Time.timeScale lúc này vẫn phải là 1 thì Anim mới chạy được
//         yield return new WaitForSeconds(2.5f);

//         // 4. HIỆN BẢNG THUA: Sau khi đợi xong mới hiện UI
//         if (UIManager.HasInstance) 
//         {
//             UIManager.Instance.ShowLose();
//         }
//     }
// }

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