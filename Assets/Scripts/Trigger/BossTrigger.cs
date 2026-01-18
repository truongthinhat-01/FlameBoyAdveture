using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;
    bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (boss != null)
                boss.SetActive(true);

            // nếu muốn xoá trigger sau khi kích hoạt
           // Destroy(gameObject);
        }
    }
}
