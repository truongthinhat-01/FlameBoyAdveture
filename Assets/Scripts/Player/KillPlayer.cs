using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            other.gameObject.GetComponent<CharacterController>().Move(Vector3.up - other.transform.position);
        }
       
    }
}
