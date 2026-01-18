// using UnityEngine;

// public class CoinArea : MonoBehaviour
// {
//      public int requiredCoinInThisArea = 4;

//      private void OnTriggerEnter(Collider other)
// {
//     if (!other.CompareTag("Player")) return;

//     CoinManager.Instance = GetComponentInParent<CoinManager>();
//     CoinManager.Instance.ResetCoin(requiredCoinInThisArea);

//     Debug.Log("➡️ QUA KHU MỚI – COIN RESET");
// }


//     // private void OnTriggerEnter(Collider other)
//     // {
//     //     if (!other.CompareTag("Player")) return;

//     //     if (CoinManager.Instance != null)
//     //     {
//     //         CoinManager.Instance.ResetCoin(requiredCoinInThisArea);
//     //     }

//     //     Debug.Log("➡️ QUA KHU MỚI – COIN RESET");
//     // }
// }



using UnityEngine;

public class CoinArea : MonoBehaviour
{
    public int requiredCoinInThisArea = 4;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        CoinManager manager = GetComponentInParent<CoinManager>();

        if (manager == null)
        {
            Debug.LogError("❌ CoinArea KHÔNG TÌM THẤY CoinManager");
            return;
        }

        manager.SetAsCurrentArea();
        manager.ResetCoin(requiredCoinInThisArea);

        Debug.Log($"➡️ VÀO KHU {manager.gameObject.name}");
    }
}
