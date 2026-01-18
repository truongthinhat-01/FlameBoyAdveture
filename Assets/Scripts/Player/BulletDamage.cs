// using UnityEngine;

// public class BulletDamage : MonoBehaviour
// {
//     bool hasHit = false;
//     Bullet vfx;

//     void Awake()
//     {
//         vfx = GetComponent<Bullet>();
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (hasHit) return;
//         hasHit = true;

//         IDamageable dmg = other.GetComponentInParent<IDamageable>();
//         if (dmg != null)
//         {
//             dmg.TakeDamage(1);
//         }

        
//          TriggerDoorwayScaffold gate = other.GetComponent<TriggerDoorwayScaffold>();
//     if (gate != null)
//     {
//         gate.Hit();
//     }

//         if (vfx != null)
//             vfx.PlayHitVFX(transform.position);
//         else
//             Destroy(gameObject);
        

        
//     }
// }
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    Bullet vfx;

    void Awake()
    {
        vfx = GetComponent<Bullet>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Enemy ăn damage
        IDamageable dmg = other.GetComponentInParent<IDamageable>();
        if (dmg != null)
        {
            dmg.TakeDamage(1);
        }

        // Cổng ăn hit
        TriggerDoorwayScaffold gate = other.GetComponent<TriggerDoorwayScaffold>();
        if (gate != null)
        {
            gate.Hit();
        }

        // VFX + destroy
        if (vfx != null)
            vfx.PlayHitVFX(transform.position);

        Destroy(gameObject);
    }
}
