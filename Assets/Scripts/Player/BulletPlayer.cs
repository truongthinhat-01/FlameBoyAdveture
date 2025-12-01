using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    
    public float speed = 20f;          // tốc độ bay của viên đạn
    public float lifeTime = 5f;        // thời gian tồn tại
    public int damage = 10;            // sát thương gây ra
    public GameObject hitEffect;       // hiệu ứng khi va chạm

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed; // cho viên đạn bay theo hướng forward
        Destroy(gameObject, lifeTime);           // tự hủy sau lifeTime giây
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Nếu đối tượng có component Health thì trừ máu
        //Health target = collision.gameObject.GetComponent<Health>();
        //if (target != null)
        //{
        //    target.TakeDamage(damage);
        //}

        // Tạo hiệu ứng va chạm
        if (hitEffect != null)
        {
            GameObject vfx = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(vfx, 2f);
        }

        // Hủy viên đạn sau khi va chạm
        Destroy(gameObject);
    }
}
