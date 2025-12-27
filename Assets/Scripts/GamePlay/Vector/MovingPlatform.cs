using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
  public float targetY = 0f; 
    public float moveSpeed = 2f; 
    private bool moving = false;

    void Update()
    {
        if (moving)
        {
            // Logic cũ: Chỉ di chuyển tọa độ Y của thang máy
            Vector3 targetPos = new Vector3(transform.position.x, targetY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

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

    // Giữ Player không bị trượt khỏi sàn (Cách cũ và an toàn nhất)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}