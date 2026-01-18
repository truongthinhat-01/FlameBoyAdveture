// using UnityEngine;

// public class Loss : MonoBehaviour
// {
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
            
//            FindFirstObjectByType<UIManager>().ShowLose();
//         }
//     }
// }


using UnityEngine;

public class Loss : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            UIManager ui = FindFirstObjectByType<UIManager>();
            if (ui != null)
            {
                ui.ShowLose();
            }
        }
    }
}
