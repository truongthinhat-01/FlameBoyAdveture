using UnityEngine;

public class FlagCollider : MonoBehaviour
{
    public MovingPlatform door;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (door != null)
            {
                door.StartMoveDown();
                
               
            }
        }
    }
}