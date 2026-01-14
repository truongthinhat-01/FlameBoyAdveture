using UnityEngine;

public class TriggerCommon : MonoBehaviour
{
     public float targetY = 3f;   // Độ cao muốn nâng tới
    public float speed = 2f;     // Tốc độ nâng

    bool isLifting = false;

    void Update()
    {
        if (!isLifting) return;

        Vector3 pos = transform.position;
        pos.y = Mathf.MoveTowards(pos.y, targetY, speed * Time.deltaTime);
        transform.position = pos;

        // Tới đích thì dừng
        if (Mathf.Abs(pos.y - targetY) < 0.01f)
            isLifting = false;
    }

    // 👉 GỌI HÀM NÀY LÀ OBJECT NÂNG LÊN
    public void LiftUp()
    {
        isLifting = true;
    }
}
