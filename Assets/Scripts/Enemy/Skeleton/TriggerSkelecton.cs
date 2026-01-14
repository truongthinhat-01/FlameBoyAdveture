using UnityEngine;

public class TriggerSkeleton : MonoBehaviour
{
    public SkeletonController skeleton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && skeleton != null)
        {
            skeleton.SpawnEnemy();
            Destroy(gameObject);
        }
    }
}