using UnityEngine;

public class ListenCoinAndMoveDown : MonoBehaviour
{
    public ColliderMoveDownY target; // object sẽ rơi
    bool triggered = false;

    void Update()
    {
        if (triggered) return;

        if (CoinManager.Instance == null) return;

        if (CoinManager.Instance.unlocked)
        {
            triggered = true;

            if (target != null)
                target.StartMoveDown();
        }
    }
}
