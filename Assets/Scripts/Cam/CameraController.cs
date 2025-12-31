using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Transform targer;
    private Vector3 offset;

    void Start()
    {
        targer = FindAnyObjectByType<PlayerController>().transform;

        offset = transform.position;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targer.position + offset, moveSpeed * Time.deltaTime);

        if (transform.position.y < offset.y)
        {
            transform.position = new Vector3(transform.position.x, offset.y, transform.position.z);
        }
    }
}
