using UnityEngine;

public class ElevatorLockPlayer : MonoBehaviour
{
    private PlayerController playerController;
    private Rigidbody rb;

    private ColliderMoveDownY elevator;

    private void Start()
    {
        elevator = GetComponent<ColliderMoveDownY>();

        // Khi thang dừng → mở khóa
       // elevator.OnElevatorStop += UnlockPlayer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        playerController = collision.collider.GetComponent<PlayerController>();
        rb = collision.collider.GetComponent<Rigidbody>();

        LockPlayer();
    }

    private void LockPlayer()
    {
        if (playerController != null)
            playerController.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Ghim vào thang
        if (playerController != null)
            playerController.transform.SetParent(transform);
    }

    private void UnlockPlayer()
    {
        if (playerController != null)
            playerController.enabled = true;

        if (rb != null)
            rb.isKinematic = false;

        if (playerController != null)
            playerController.transform.SetParent(null);
    }
}
