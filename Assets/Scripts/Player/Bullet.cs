using UnityEngine;

public class Bullet : MonoBehaviour
{
    //VFX+Huy dan
    public GameObject hitEffect;
     private bool hasHit = false;
    //  private void OnTriggerEnter(Collider other)
    // {
    //     if (hasHit) return;

    //     // Nếu va chạm bất kỳ collider nào (enemy, tường, đất…)
    //     SpawnVFX();
    //     Destroy(gameObject);
    //     hasHit = true;
    // }

    // void SpawnVFX()
    // {
    //     if (hitEffect != null)
    //     {
    //         GameObject vfx = Instantiate(hitEffect, transform.position, Quaternion.identity);
    //         Destroy(vfx, 2f);
    //     }
    // }
    public void PlayHitVFX(Vector3 pos)
    {
        if (hasHit) return;
        hasHit = true;

        if (hitEffect)
            Instantiate(hitEffect, pos, Quaternion.identity);

        Destroy(gameObject);
    }

}
