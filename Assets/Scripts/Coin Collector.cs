using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    PlayerController playerCon;

    [SerializeField] GameObject coinCollectFX;
    private void Start()
    {
        playerCon = GetComponent<PlayerController>();
    }
    //void CreateCollectFX(Vector3 position)
    //{
    //    Instantiate(coinCollectFX,position,Quaternion.identity);
        
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
           // CreateCollectFX(other.transform.position + Vector3.up * 0.7f);
          
           // playerCon.coinCollectFX.Invoke();
            other.gameObject.SetActive(false);
        }
    }
}
