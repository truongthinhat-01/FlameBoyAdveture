using UnityEngine;

public class DisableColliderReusable : MonoBehaviour
{
    [Header("Collider muốn tắt")]
    public Collider targetCollider;

    [Header("Tùy chọn")]
    public bool disableOnStart = false;   // Tự động tắt khi bắt đầu
    public float delay = 0f;              // Tắt sau delay (s)

    private void Start()
    {
        if (disableOnStart && targetCollider != null)
        {
            if (delay > 0f)
                Invoke(nameof(Disable), delay);
            else
                Disable();
        }
    }

    // 🔹 Hàm tắt collider có thể gọi từ script khác
    public void Disable()
    {
        if (targetCollider != null)
        {
            targetCollider.enabled = false;
            Debug.Log($"Collider {targetCollider.name} đã bị tắt");
        }
    }

    // 🔹 Hàm bật lại collider nếu muốn
    public void Enable()
    {
        if (targetCollider != null)
        {
            targetCollider.enabled = true;
            Debug.Log($"Collider {targetCollider.name} đã được bật lại");
        }
    }
     public void Hide()
    {
        gameObject.SetActive(false);
        Debug.Log($"{gameObject.name} đã bị ẩn bởi Skeleton");
    }
}
