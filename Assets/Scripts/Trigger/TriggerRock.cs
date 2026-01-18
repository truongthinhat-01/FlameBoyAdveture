using UnityEngine;

public class TriggerRock : MonoBehaviour
{
   
    bool rock = false;
    void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if(!rock) return;
        if(CoinManager.Instance == null) return;
        if (CoinManager.Instance.unlocked)
        {
            ShowRock();
        }
    }
    void ShowRock()
    {
        rock = true;
        gameObject.SetActive(true);

    }

}
