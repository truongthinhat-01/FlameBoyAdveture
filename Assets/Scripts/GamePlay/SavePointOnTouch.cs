using UnityEngine;

public class SavePointOnTouch : MonoBehaviour
{
     public Vector3 savedPoint;   // điểm lưu sau khi player chạm

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            savedPoint = other.transform.position;
            Debug.Log("Saved point: " + savedPoint);
        }
    }
}
