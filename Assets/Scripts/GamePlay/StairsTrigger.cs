using UnityEngine;

public class StairsTrigger : MonoBehaviour
{
    public StairController stair;

    public void OnEnemyDie()
    {
        if (stair != null)
            stair.OnPlayerComplete();
    }
    
    
   
}
