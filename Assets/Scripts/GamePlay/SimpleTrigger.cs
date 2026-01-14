using UnityEngine;

public class SimpleTrigger : MonoBehaviour
{ 
    public TriggerCommon stair; // kéo stair vào Inspector
private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        stair.LiftUp();
    }
}

}
