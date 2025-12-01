using UnityEngine;

public class KeyController : MonoBehaviour
{
    public static KeyController Instance;

    [Header("Key Settings")]
    public int currentKey = 0;       // số key hiện tại
    public int requiredKey = 1;      // số key cần mở cầu thang

    [Header("Stair")]
    public StairController stair;    // kéo cầu thang vào đây

    private void Awake()
    {
        Instance = this;
    }

    // Gọi khi Player nhặt key
    public void AddKey(int amount = 1)
    {
        currentKey += amount;

        if (currentKey >= requiredKey)
        {
            stair.OnPlayerComplete(); // gọi cầu thang trồi lên
        }
    }
}
