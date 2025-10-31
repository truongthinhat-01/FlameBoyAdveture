using UnityEngine;

public class RotaY : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 180f; // độ/giây
    [SerializeField] private float rotateAngle = -90f; // góc xoay (độ)
    [SerializeField] private float waitTime = 0.5f;    // thời gian chờ trước khi quay ngược

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private bool rotatingForward = false;
    private bool rotatingBack = false;
    private bool rotated = false; // đã xoay sang hướng mới chưa
    private float timer = 0f;

    void Start()
    {
        startRotation = transform.rotation; // lưu góc ban đầu
        targetRotation = startRotation * Quaternion.Euler(0f, rotateAngle, 0f);
    }

    void Update()
    {
        // Quay tới góc 90°
        if (rotatingForward)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                rotatingForward = false;
                rotated = true;
                timer = waitTime; // đợi trước khi có thể xoay lại
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
                rotated = false;
                timer = waitTime;
            }
        }

        // Giảm thời gian chờ
        if (timer > 0)
            timer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Khi Player chạm trigger
        if (other.CompareTag("Player") && timer <= 0f)
        {
            if (!rotated && !rotatingForward && !rotatingBack)
            {
                // Lần đầu → xoay tới
                rotatingForward = true;
            }
            else if (rotated && !rotatingForward && !rotatingBack)
            {
                // Lần thứ hai → xoay ngược lại
                rotatingBack = true;
            }
        }
    }
}
