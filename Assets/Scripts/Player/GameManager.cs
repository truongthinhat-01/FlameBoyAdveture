using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
using System.Collections; // Namespace mới cho Cinemachine 3

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Settings")]
    public GameObject playerPrefab;
    private GameObject playerInstance;

    [Header("Camera Settings")]
    // Không cần kéo thả trong Inspector nữa, Code sẽ tự tìm
    [SerializeField] private CinemachineCamera cinemachineCamera; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1. Tìm Camera của Scene vừa load
        FindCameraInScene();
        // 2. Spawn Player
        SpawnPlayer();
    }

    void FindCameraInScene()
    {
        // Tìm CinemachineCamera bất kỳ đang có mặt trong Scene mới
        cinemachineCamera = GameObject.FindAnyObjectByType<CinemachineCamera>();
        
        if (cinemachineCamera == null)
        {
            Debug.LogWarning("⚠️ Scene này thiếu Cinemachine Camera Prefab!");
        }
    }

    void SpawnPlayer()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        if (spawnPoint == null)
        {
            Debug.LogError("❌ Không tìm thấy PlayerSpawn Tag trong Scene!");
            return;
        }

        // Xóa Player cũ nếu có để tránh nhân bản
        if (playerInstance != null) Destroy(playerInstance);

        playerInstance = Instantiate(
            playerPrefab, 
            spawnPoint.transform.position, 
            spawnPoint.transform.rotation
        );

        // Sau khi có Player, kết nối với Camera
        StartCoroutine(AssignCameraTarget());
    }

    IEnumerator AssignCameraTarget()
    {
        yield return null;

        if (cinemachineCamera == null)
        FindCameraInScene();

        if (cinemachineCamera == null || playerInstance == null)
        yield break;

        // Tìm điểm Follow trên người Player
        Transform followPoint = playerInstance.transform.Find("CameraFollowPoint");

        if (followPoint != null)
        {
            // ✅ Gán mục tiêu cho CM3 - Tự động hết báo vàng
            cinemachineCamera.Target.TrackingTarget = followPoint;
            cinemachineCamera.Target.LookAtTarget = followPoint;
            Debug.Log("✅ Đã kết nối Camera thành công!");
        }
        else
        {
            // Nếu quên tạo điểm Follow, gán đại vào Player để khỏi lỗi
            cinemachineCamera.Target.TrackingTarget = playerInstance.transform;
            Debug.LogWarning("⚠️ Player thiếu CameraFollowPoint, đang gán vào gốc nhân vật.");
        }
    }
}