// using UnityEngine;

// public class CoinCollector : MonoBehaviour
// {
//     CoinManager coinManager;

//     [SerializeField] GameObject coinCollectFX;

//      void CreateCollectFX(Vector3 pos)
//     {
//         if (coinCollectFX != null)
//         {
//             Instantiate(
//                 coinCollectFX,
//                 pos + Vector3.up * 0.7f,
//                 Quaternion.identity
//             );
//         }
//     }
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Coin"))
//         {
//             CreateCollectFX(other.transform.position + Vector3.up * 0.7f);

           
//             CoinManager.Instance.AddCoin(1);

//             other.gameObject.SetActive(false);
//         }
        
//     }
// }


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
        if (!other.CompareTag("Coin")) return;

        CreateCollectFX(other.transform.position);

        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoin(1);
        }
        else
        {
            Debug.LogWarning("CHƯA VÀO KHU – KHÔNG CỘNG COIN");
        }

        other.gameObject.SetActive(false);
    }
}
