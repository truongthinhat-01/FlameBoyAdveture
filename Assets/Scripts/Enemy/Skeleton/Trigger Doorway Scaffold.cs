using UnityEngine;

public class TriggerDoorwayScaffold : MonoBehaviour
{
 public int hitToBreak = 3;
    private int hitCount = 0;

    public void Hit()
    {
        hitCount++;

        if (hitCount >= hitToBreak)
        {
            Destroy(gameObject);
        }
    }
}
