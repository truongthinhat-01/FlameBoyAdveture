using UnityEngine;
using System.Collections;

public class SimpleMovingTrap : MonoBehaviour
{  
     [Header("Spin")]
    public Vector3 spinAxis = Vector3.up;
    public float spinSpeed = 80f;

    [Header("Move PingPong")]
    public bool enableMove = true;
    public float moveDistance = 2f;
    public float moveSpeed = 2f;

    [Header("Direction")]
    public bool moveHorizontal = true; // X = true, Z = false

    private Vector3 startLocalPos;

    void Start()
    {
        // Lưu vị trí LOCAL để không bị lệch khi Play
        startLocalPos = transform.localPosition;
    }

    void Update()
    {
        // Xoay quanh chính mình
        transform.Rotate(spinAxis, spinSpeed * Time.deltaTime);

        if (enableMove)
        {
            float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

            if (moveHorizontal)
                transform.localPosition = startLocalPos + new Vector3(offset, 0, 0);
            else
                transform.localPosition = startLocalPos + new Vector3(0, 0, offset);
        }
    }
    private void OggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }

        }
    }
}
