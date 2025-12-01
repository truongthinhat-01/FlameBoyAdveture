using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>()?.TakeDamage();
            Destroy(gameObject);
        }

        if (hitEffect != null)
        {
            GameObject vfx = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(vfx, 2f);
        }

        // Hủy viên đạn sau khi va chạm
        Destroy(gameObject);
    }
}
