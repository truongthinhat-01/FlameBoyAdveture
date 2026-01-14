using UnityEngine;

public class ElevatorUp : MonoBehaviour
{
     public Transform platform;       // platform tron
    public float moveHeight = 6f;     // do cao di len
    public float speed = 2f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool isMoving = false;

    private void Start()
    {
        startPos = platform.position;
        targetPos = startPos + Vector3.up * moveHeight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;

            // gan player vao platform
            other.transform.SetParent(platform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    private void Update()
    {
        if (!isMoving) return;

        platform.position = Vector3.MoveTowards(
            platform.position,
            targetPos,
            speed * Time.deltaTime
        );
    }
}
