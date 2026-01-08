using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    
    public GameObject playerPrefab;
    private GameObject playerInstance;
    void Awake()
    {
        if(Instance == null)
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
    void OnSceneLoaded(Scene scene,LoadSceneMode mode
)
    {
        SpawPlayer();
    }
    void SpawPlayer()
    {
        GameObject spawPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        if(spawPoint == null) return;

        if(playerInstance != null)
        {
            Destroy(playerInstance);
        }

        playerInstance = Instantiate(playerPrefab,
        spawPoint.transform.position,
        spawPoint.transform.rotation
        );

         AssignCameraTarget(playerInstance.transform);
    }

     void AssignCameraTarget(Transform player)
{
    CinemachineCamera vcam = FindAnyObjectByType<CinemachineCamera>();

    if (vcam != null)
    {
        vcam.Target.TrackingTarget = player;
    }
}

}
