using UnityEngine;

public class FlagCollider : MonoBehaviour
{
   public ColliderMoveDownY door;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.StartMoveDown();
           // gameObject.SetActive(false);
        }
    }
}
