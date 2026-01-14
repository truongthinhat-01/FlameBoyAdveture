using UnityEngine;

public class TriggerStartCollider : MonoBehaviour
{
   public GameObject collider; // cau thang

    private void Start()
    {
        collider.SetActive(false); // an ban dau
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collider.SetActive(true); // hien cau thang
        }
    }
}
