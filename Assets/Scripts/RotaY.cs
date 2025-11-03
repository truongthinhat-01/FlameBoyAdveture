using UnityEngine;

public class RotaY : MonoBehaviour
{
    [SerializeField] private float rotateAngle = 90f;
    [SerializeField] private float rotateSpeed = 180f; // độ/giây
    [SerializeField] private string playerTag = "Player";

    private Quaternion targetRotation;
    private bool rotating = false;

    private void Start()
    {
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        if (rotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                rotating = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, rotateAngle, 0));
            rotating = true;
        }
    }

}
