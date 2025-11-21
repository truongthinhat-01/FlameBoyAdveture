//using System.Collections;
//using UnityEngine;

//public class RotaY : MonoBehaviour
//{


//    [SerializeField] private float rotateAngle = 90f;   // góc xoay
//    [SerializeField] private float rotateSpeed = 180f;  // độ/giây
//    private bool isRotating = false;

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player") && !isRotating)
//        {
//            StartCoroutine(RotateY());
//        }
//    }

//    private IEnumerator RotateY()
//    {
//        isRotating = true;

//        Quaternion startRot = transform.rotation;
//        Quaternion endRot = Quaternion.Euler(
//            startRot.eulerAngles.x,
//            startRot.eulerAngles.y + rotateAngle,
//            startRot.eulerAngles.z
//        );

//        float time = 0f;
//        float duration = Mathf.Abs(rotateAngle / rotateSpeed); // thời gian xoay

//        while (time < duration)
//        {
//            time += Time.deltaTime;
//            float t = Mathf.Clamp01(time / duration);
//            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
//            yield return null;
//        }

//        transform.rotation = endRot;
//        isRotating = false;
//    }
//}
using System.Collections;
using UnityEngine;

public class RotaY : MonoBehaviour
{
    [SerializeField] private float rotateAngle = 90f;     // góc xoay
    [SerializeField] private float rotateDuration = 0.6f; // thời gian xoay (0.6s = chậm đẹp)

    private bool isRotating = false;
    private bool rotated = false; // lần 1 xoay, lần 2 xoay ngược

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRotating)
        {
            StartCoroutine(RotateSmooth());
        }
    }

    private IEnumerator RotateSmooth()
    {
        isRotating = true;

        Quaternion startRot = transform.rotation;
        Quaternion endRot;

        if (!rotated)
        {
            endRot = Quaternion.Euler(startRot.eulerAngles.x,
                                      startRot.eulerAngles.y + rotateAngle,
                                      startRot.eulerAngles.z);
        }
        else
        {
            endRot = Quaternion.Euler(startRot.eulerAngles.x,
                                      startRot.eulerAngles.y - rotateAngle,
                                      startRot.eulerAngles.z);
        }

        float time = 0f;

        while (time < rotateDuration)
        {
            time += Time.deltaTime;

            // t dùng để làm smooth
            float t = time / rotateDuration;
            t = Mathf.SmoothStep(0f, 1f, t); // chuyển động mượt

            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }

        transform.rotation = endRot;

        rotated = !rotated; // đổi trạng thái để lần sau xoay ngược
        isRotating = false;
    }
}


