using UnityEngine;

public class ColliderPositionY : MonoBehaviour
{
    [Header("Vị trí đích trên trục Y")]
    public float targetY = 5f; // Vị trí Y muốn đến

    [Header("Tốc độ di chuyển")]
    public float moveSpeed = 2f; // Tốc độ di chuyển lên

    [Header("Tùy chọn")]
    public bool moveUp = false; // Bật true để bắt đầu di chuyển

    private Vector3 startPos;

    void Start()
    {
        // Lưu vị trí ban đầu
        startPos = transform.position;
    }

    void Update()
    {
        if (moveUp)
        {
            // Di chuyển dần đến vị trí đích
            Vector3 targetPos = new Vector3(startPos.x, targetY, startPos.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            // Khi chạm đích thì dừng
            if (Mathf.Abs(transform.position.y - targetY) < 0.01f)
            {
                moveUp = false;
            }
        }
    }

    // Có thể gọi hàm này từ script khác hoặc animation event
    public void StartMoveUp()
    {
        moveUp = true;
    }
}
