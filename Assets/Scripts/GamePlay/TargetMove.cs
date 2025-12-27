using UnityEngine;

public class TargetMove : MonoBehaviour
{
     public ColliderMoveDownY door;

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