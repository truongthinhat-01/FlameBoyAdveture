using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
    public Transform spikesMesh; // Kéo object Spikes vào
    public float activeHeight = 0f;   // vị trí khi trồi lên
    public float hideHeight = -0.5f;  // vị trí khi ẩn
    public float moveTime = 0.3f;
    public float stayUpTime = 0.5f;

    bool isDanger = false;


    private void Start()
    {
        StartCoroutine(SpikeRoutine());
    }

    IEnumerator SpikeRoutine()
    {
        while (true)
        {
            
            // Trồi lên
            isDanger = true;
            yield return MoveSpike(activeHeight);

            // Giữ nguyên trên cao
            yield return new WaitForSeconds(stayUpTime);

            // Thụt xuống
            isDanger = false;
            yield return MoveSpike(hideHeight);

            // Chờ 1s rồi lặp
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator MoveSpike(float targetY)
    {
        Vector3 start = spikesMesh.localPosition;
        Vector3 end = new Vector3(0, targetY, 0);
        float t = 0f;

        while (t < moveTime)
        {
            t += Time.deltaTime;
            spikesMesh.localPosition = Vector3.Lerp(start, end, t / moveTime);
            yield return null;
        }
        spikesMesh.localPosition = end;
    }
    private void OnTriggerEnter(Collider other)
{
    if (!isDanger) return;  // Nếu đang ẩn thì không gây damage

    if (other.CompareTag("Player"))
    {
        PlayerHealth hp = other.GetComponent<PlayerHealth>();

        if (hp != null)
        {
             Debug.Log("Trap HIT! Damage: " + 1);
            hp.TakeDamage(1);
        }
    }
}

}
