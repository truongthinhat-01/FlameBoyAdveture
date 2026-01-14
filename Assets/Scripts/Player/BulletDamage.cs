using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    bool hasHit = false;
    Bullet vfx;

    void Awake()
    {
        vfx = GetComponent<Bullet>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        hasHit = true;

        IDamageable dmg = other.GetComponentInParent<IDamageable>();
        if (dmg != null)
        {
            dmg.TakeDamage(1);
        }

        if (vfx != null)
            vfx.PlayHitVFX(transform.position);
        else
            Destroy(gameObject);
    }
}
