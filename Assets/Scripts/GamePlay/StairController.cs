using UnityEngine;

public class StairController : MonoBehaviour
{

    public float targetY = 0f;

    public float riseSpeed = 2f;

    private bool shouldRise = false;

    void Update()
    {
       
        if (shouldRise)
        {
      
            Vector3 pos = transform.position;
            pos.y = Mathf.MoveTowards(pos.y, targetY, riseSpeed * Time.deltaTime);
            transform.position = pos;
        }
    }

    public void OnPlayerComplete()
    {
        shouldRise = true;
    }
}
