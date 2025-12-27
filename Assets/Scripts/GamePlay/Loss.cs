using UnityEngine;

public class Loss : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
           // FindFirstObjectByType<UIManager>().ShowLoss();
        }
    }
}
