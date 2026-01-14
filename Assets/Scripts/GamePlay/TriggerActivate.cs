using UnityEngine;

public class TriggerActivate : MonoBehaviour
{
    public MoveAxisController moviA;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moviA.OnPlayerComplete();
        }
    }

   
}
