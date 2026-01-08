using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Khoảng cách camera so với player
    public Vector3 offset = new Vector3(0, 5, -7);

    // Độ mượt khi camera di chuyển
    public float smoothSpeed = 5f;

    // Transform của player
    private Transform player;

    void LateUpdate()
    {
        // Nếu chưa tìm được player
        if (player == null)
        {
            // Tìm player theo Tag
            GameObject p = GameObject.FindGameObjectWithTag("Player");

            if (p != null)
            {
                player = p.transform;
            }

            return;
        }

        // Vị trí camera mong muốn
        Vector3 targetPosition = player.position + offset;

        // Di chuyển camera mượt theo player
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}
