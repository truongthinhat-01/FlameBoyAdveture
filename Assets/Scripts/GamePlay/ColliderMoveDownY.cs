using UnityEngine;

public class ColliderMoveDownY : MonoBehaviour
{
    [Header("Vị trí đích trên trục Y (thấp hơn)")]
    public float targetY = 0f; // Vị trí Y muốn đến (thấp hơn vị trí ban đầu)

    [Header("Tốc độ di chuyển")]
    public float moveSpeed = 2f; // Tốc độ di chuyển xuống

    private Vector3 startPos;

    void Start()
    {
        // Lưu vị trí ban đầu
        startPos = transform.position;
    }

private bool moving = false;

void Update()
{
    if (moving)
    {
        Vector3 targetPos = new Vector3(transform.position.x, targetY, transform.position.z);

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        if (Mathf.Abs(transform.position.y - targetY) < 0.01f)
        {
            moving = false;
        }
    }
}

public void StartMoveDown()
{
    moving = true;
}

}
