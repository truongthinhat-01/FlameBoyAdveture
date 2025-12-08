using UnityEngine;

public class ElevatorAttach : MonoBehaviour
{ public float targetY = 0f;
    public float moveSpeed = 2f;

    private bool moving = false;

    public System.Action OnElevatorStop;  // Gọi khi thang dừng

    private void Update()
    {
        if (!moving) return;

        Vector3 targetPos = new Vector3(transform.position.x, targetY, transform.position.z);

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        if (Mathf.Abs(transform.position.y - targetY) < 0.01f)
        {
            moving = false;

            // Báo rằng thang đã dừng
            OnElevatorStop?.Invoke();
        }
    }

    public void StartMoveDown()
    {
        moving = true;
    }
}
