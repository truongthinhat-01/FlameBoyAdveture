using UnityEngine;

public class Key1 : MonoBehaviour
{
    
    public StairController rock;
    public void OTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rock.OnPlayerComplete();
            gameObject.SetActive(false);
        }
    }
}
