using UnityEngine;

public class Loss : MonoBehaviour
{
     public GameObject lossPanel;   // Kéo Panel Win vào

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lossPanel.SetActive(true);    // Hiện panel win
            Time.timeScale = 0f;         // Pause game
        }
    }
}
