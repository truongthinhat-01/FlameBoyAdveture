using UnityEngine;

public class TriggerSkelecton : MonoBehaviour
{
    SkeletonController skeleton;

    private void Awake() {
        if (skeleton == null) {
            skeleton = FindAnyObjectByType<SkeletonController>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            skeleton.WakeUp();
            Destroy(gameObject);
        }
    }
}
