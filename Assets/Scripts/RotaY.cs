using UnityEngine;

public class RotaY : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 180f; // độ/giây
    [SerializeField] private float rotateAngle = -90f;  // góc xoay (độ)
    [SerializeField] private float waitTime = 0.5f;    // thời gian chờ trước khi quay ngược

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private bool rotatingForward = false;
    private bool rotatingBack = false;
    private float timer = 0f;

    void Start()
    {
        startRotation = transform.rotation; // lưu góc ban đầu
    }

    void Update()
    {
        // Nhấn Space để bắt đầu xoay 90°
        if (Input.GetKeyDown(KeyCode.Space) && !rotatingForward && !rotatingBack)
        {
            targetRotation = startRotation * Quaternion.Euler(0f, rotateAngle, 0f);
            rotatingForward = true;
        }

        // Quay tới góc 90°
        if (rotatingForward)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                rotatingForward = false;
                timer = waitTime; // đợi trước khi quay lại
            }
        }

        // Chờ một chút trước khi quay ngược
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                rotatingBack = true;
            }
        }

        // Quay trở về vị trí ban đầu
        if (rotatingBack)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, rotateSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, startRotation) < 0.1f)
            {
                transform.rotation = startRotation;
                rotatingBack = false;
            }
        }
    }
}
