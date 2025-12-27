using UnityEngine;

public class Win : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (UIManagerEvent.HasInstance)
            {
                UIManagerEvent.Instance.WinGame();
            }
        }
    }
}
