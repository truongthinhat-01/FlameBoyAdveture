using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [Header("Condition")]
    public int requiredCoin = 5;

    [Header("Enemy To Spawn")]
    public SkeletonController skeleton;   // Giant / Skeleton / Boss

    bool spawned;

    private void Start()
    {
        if (skeleton != null)
            skeleton.gameObject.SetActive(false); // ✅

    }

    private void OnTriggerEnter(Collider other)
    {
        if (spawned) return;
        if (!other.CompareTag("Player")) return;

        if (CoinManager.Instance.currentCoin >= requiredCoin)
        {
            SpawnEnemy();
        }
        else
        {
            Debug.Log("❌ Chưa đủ coin");
        }
    }

    void SpawnEnemy()
{
    spawned = true;

    if (skeleton != null)
    {
        skeleton.Spawn();
        Debug.Log("👹 SPAWN ENEMY");
    }
}

}
