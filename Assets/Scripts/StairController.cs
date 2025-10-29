using UnityEngine;

public class StairController : MonoBehaviour
{
    // V? trí tr?i lên m?t ??t
    public float targetY = 0f;

    // T?c ?? tr?i lên
    public float riseSpeed = 2f;

    private bool shouldRise = false;

    void Update()
    {
        // N?u ng??i ch?i ?ã hoàn thành
        if (shouldRise)
        {
            // Di chuy?n d?n lên v? trí mong mu?n
            Vector3 pos = transform.position;
            pos.y = Mathf.MoveTowards(pos.y, targetY, riseSpeed * Time.deltaTime);
            transform.position = pos;
        }
    }

    // Hàm này g?i khi ng??i ch?i hoàn thành nhi?m v?
    public void OnPlayerComplete()
    {
        shouldRise = true;
    }
}
