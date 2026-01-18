using UnityEngine;

public class SkeDieTrgger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public ColliderMoveDownY objrock;


   public void OnDown()
    {
        if(objrock != null)
        {
            objrock.StartMoveDown();
        }
    }
}
