using UnityEngine;

public class Win : MonoBehaviour
{
     public GameObject winPanel;   // Kéo Panel Win vào

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winPanel.SetActive(true);    // Hiện panel win
            Time.timeScale = 0f;         // Pause game
        }
    }
}
