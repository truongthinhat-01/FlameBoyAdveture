using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<GiantEnemyController>()?.TakeDamage();
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
    private void OnCollisionEnter(Collision collision)
{
    if(collision.collider.CompareTag("Enemy"))
    {
        collision.collider.GetComponent<GiantEnemyController>().TakeDamage();
    }

    Destroy(gameObject);
}

}
