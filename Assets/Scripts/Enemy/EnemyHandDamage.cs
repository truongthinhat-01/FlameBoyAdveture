// using UnityEngine;
// using System.Collections;

// public class EnemyHandDamage : MonoBehaviour
// {
//     private void OnTriggerEnter(Collider other)
//     {
//         // Kiểm tra đúng Tag "Player"
//         if (other.CompareTag("Player"))
//         {
//             PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
//             if (playerHealth != null)
//             {
//                 playerHealth.TakeDamage(1); // Gọi trực tiếp hàm TakeDamage trên Player
//                 Debug.Log("Đã đấm trúng Player!");
                
//                 // Tắt tạm thời và TỰ ĐỘNG bật lại sau 0.8s để có thể đấm phát tiếp theo
//                 StartCoroutine(ResetHandCollider());
//             }
//         }
//     }

//     IEnumerator ResetHandCollider()
//     {
//         Collider col = GetComponent<Collider>();
//         if (col != null)
//         {
//             col.enabled = false; 
//             yield return new WaitForSeconds(0.8f); // Thời gian chờ giữa 2 lần nhận dame
//             col.enabled = true;
//             Debug.Log("Tay quái vật đã sẵn sàng đánh tiếp");
//         }
//     }
// }


// // using UnityEngine;

// // public class EnemyHandDamage : MonoBehaviour
// // {
// //     public int damage = 1;
// //     bool hasHit; // chặn đánh nhiều lần trong 1 attack

// //     private void OnTriggerEnter(Collider other)
// //     {
// //         if (hasHit) return;

// //         if (other.CompareTag("Player"))
// //         {
// //             PlayerHealth hp = other.GetComponent<PlayerHealth>();
// //             if (hp != null)
// //             {
// //                 hp.TakeDamage(damage);
// //                 hasHit = true;
// //                 Debug.Log("💥 Enemy đánh trúng Player");
// //             }
// //         }
// //     }

// //     // ===== GỌI BẰNG ANIMATION EVENT =====
// //     public void ResetHit()
// //     {
// //         hasHit = false;
// //     }
// // }


// using UnityEngine;

// public class EnemyHandDamage : MonoBehaviour
// {
//     private void OnTriggerEnter(Collider other)
//     {
//         Debug.Log("🟢 Tay va cham voi: " + other.name);

//         if (other.CompareTag("Player"))
//         {
//             PlayerHealth hp = other.GetComponent<PlayerHealth>();
//             if (hp != null)
//             {
//                 hp.TakeDamage(1);
//                 Debug.Log("🔥 PLAYER NHẬN DAMAGE");
//             }
//         }
//     }
// }


using UnityEngine;

public class EnemyHandDamage : MonoBehaviour
{
    void Update()
    {
        Debug.Log("🔥 EnemyHandDamage Update đang chạy");
    }

   private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("💥 Tay enemy chạm Player");

        PlayerHealth hp = other.GetComponent<PlayerHealth>();
        if (hp != null)
        {
            hp.TakeDamage(1);
        }
    }
}

}


