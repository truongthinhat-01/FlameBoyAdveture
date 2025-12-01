using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KeyController.Instance.AddKey(1);  // tăng key
            other.gameObject.SetActive(false);        // nhặt key
        }
    }
}
