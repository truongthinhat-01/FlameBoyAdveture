using UnityEngine;

public class MoveAxisController : MonoBehaviour
{
     [Header("Move Settings")]
    public float moveDistance = 3f;   // Đi bao xa theo hướng đang nhìn
    public float moveSpeed = 2f;      // Tốc độ di chuyển

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool shouldMove = false;

    void Start()
    {
        // Lưu vị trí ban đầu
        startPos = transform.position;

        // Tính vị trí đích theo hướng object đang nhìn
        targetPos = startPos + transform.forward * moveDistance;
    }

    void Update()
    {
        if (!shouldMove) return;

        // Di chuyển về phía target
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        // Tới đích thì dừng
        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            shouldMove = false;
        }
    }

    public void OnPlayerComplete()
    {
        shouldMove = true;
    }
}
