using UnityEngine;

public class CoinCollector : MonoBehaviour
{

    [SerializeField] GameObject coinCollectFX;

     void CreateCollectFX(Vector3 pos)
    {
        if (coinCollectFX != null)
        {
            Instantiate(
                coinCollectFX,
                pos + Vector3.up * 0.7f,
                Quaternion.identity
            );
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            CreateCollectFX(other.transform.position + Vector3.up * 0.7f);

           
            CoinManager.Instance.AddCoin(1);

            other.gameObject.SetActive(false);
        }
        
    }
}
