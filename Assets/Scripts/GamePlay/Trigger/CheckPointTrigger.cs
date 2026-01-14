using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
     public Transform targetPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false; // tat controller
                other.transform.position = targetPoint.position;
                cc.enabled = true; // bat lai
            }
        }
    }
}
