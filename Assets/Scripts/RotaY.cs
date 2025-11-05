using System.Collections;
using UnityEngine;

public class RotaY : MonoBehaviour
{


    [SerializeField] private float rotateAngle = 90f;   // góc xoay
    [SerializeField] private float rotateSpeed = 180f;  // độ/giây
    private bool isRotating = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRotating)
        {
            StartCoroutine(RotateY());
        }
    }

    private IEnumerator RotateY()
    {
        isRotating = true;

        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(
            startRot.eulerAngles.x,
            startRot.eulerAngles.y + rotateAngle,
            startRot.eulerAngles.z
        );

        float time = 0f;
        float duration = Mathf.Abs(rotateAngle / rotateSpeed); // thời gian xoay

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }

        transform.rotation = endRot;
        isRotating = false;
    }
}

