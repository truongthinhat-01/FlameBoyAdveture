using UnityEngine;

public class CoinAnimator : MonoBehaviour
{
    [SerializeField] private float AngularSpeed = 50f;
    [SerializeField] private float coinHeight = 0.7f;
    [SerializeField] private float MovementAmplitude = 0.5f;
    [SerializeField] private float MovementFrequency = 1f;

    private Vector3 startLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
    }

    void Update()
    {
        // Xoay coin quanh trục Y, tại chỗ
        transform.Rotate(0f, AngularSpeed * Time.deltaTime, 0f);

        // Nhấp nhô lên xuống
        float deltaY = MovementAmplitude * Mathf.Sin(MovementFrequency * Time.time);
        transform.localPosition = startLocalPos + new Vector3(0f, coinHeight + deltaY, 0f);
    }
}
