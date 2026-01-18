using UnityEngine;

public class Bullet : MonoBehaviour
{
    //VFX+Huy dan
    public GameObject hitEffect;
     private bool hasHit = false;
    public void PlayHitVFX(Vector3 pos)
    {
        if (hasHit) return;
        hasHit = true;

        if (hitEffect)
            Instantiate(hitEffect, pos, Quaternion.identity);

        Destroy(gameObject);
    }

}
