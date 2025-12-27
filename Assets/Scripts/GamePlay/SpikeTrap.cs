// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.Rendering;

// public class SpikeTrap : MonoBehaviour
// {
//     [SerializeField] float activeDuration = 0.5f;
//     [SerializeField] float transitionDuration = 0.2f;

//     [SerializeField] Vector3 spikesActivePosition = Vector3.zero;
//     [SerializeField] Vector3 spikesIdlePosition = new Vector3(0f,-0.5f,0f);

//     [Header("Sounds")]
    
//     [SerializeField] AudioClip activationSound;

//     [Header("Components")]
//     [SerializeField] ParticleSystem activationEffect;
//     [SerializeField] AudioSource audioSource;
//     [SerializeField] GameObject spikesMesh;

//     private float timer;

//     enum EState
//     {
//         Idle,
//         TransitionToActive,
//         Active,
//         TransitionToIdle
//     }
    

//     EState state =  EState.Idle;

//     void ChangeState(EState newState)
//     {

//         state = newState;
//         timer = 0f;
//         if(state == EState.Idle)
//         {
//             spikesMesh.SetActive(false);
//         }
//         else
//         {
//             spikesMesh.SetActive(true);
//         }
//         if(state == EState.TransitionToActive)
//         {
//             activationEffect.Play();
//         }
//     }


//     private void Start()
//     {
//         spikesMesh.SetActive(false);
//     }


//     private void Update()
//     {
//         if(state == EState.TransitionToActive)
//         {
//             Vector3 p = Vector3.Lerp(spikesIdlePosition,spikesActivePosition,timer / transitionDuration);
//             spikesMesh.transform.localPosition = p;
//             if(timer >= transitionDuration)
//             {
//                 ChangeState(EState.Active);
//             }
//         }
//         else if(state == EState.TransitionToIdle)
//         {
//             Vector3 p = Vector3.Lerp(spikesActivePosition,spikesIdlePosition,timer / transitionDuration);
//             spikesMesh.transform.localPosition = p;
//              if(timer >= transitionDuration)
//             {
//                 ChangeState(EState.Idle);
//             }
//         }
//         else if(state == EState.Active)
//         {
//             if(timer >= activeDuration)
//             {
//                 ChangeState(EState.TransitionToIdle);
//             }
//         }
//         timer += Time.deltaTime;
//     }


//     [ContextMenu("Activate Spikes Trap")]
//     public void Activate()
//     {
//         if(state == EState.Idle)
//         {
//             ChangeState(EState.TransitionToActive);
//         }
      
//     }


//     void HideSpikes()
//     {
//         spikesMesh.SetActive(false);
//     }
// }


using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] float activeDuration = 1.0f; // Thời gian chông đứng yên trên cao
    [SerializeField] float transitionDuration = 0.2f; // Thời gian chông nhô lên/thụt xuống

    [SerializeField] Vector3 spikesActivePosition = Vector3.zero;
    [SerializeField] Vector3 spikesIdlePosition = new Vector3(0f, -0.5f, 0f);

    [Header("Components")]
    [SerializeField] GameObject spikesMesh;
    [SerializeField] ParticleSystem activationEffect;

    private bool isMovingUp = false;
    private bool isMovingDown = false;
    private float moveTimer = 0f;

    void Update()
    {
        // 1. Xử lý di chuyển lên
        if (isMovingUp)
        {
            moveTimer += Time.deltaTime;
            spikesMesh.transform.localPosition = Vector3.Lerp(spikesIdlePosition, spikesActivePosition, moveTimer / transitionDuration);
            
            if (moveTimer >= transitionDuration)
            {
                isMovingUp = false;
                // Đợi 1 khoảng thời gian (activeDuration) rồi tự thụt xuống
                Invoke("StartLowering", activeDuration); 
            }
        }

        // 2. Xử lý di chuyển xuống
        if (isMovingDown)
        {
            moveTimer += Time.deltaTime;
            spikesMesh.transform.localPosition = Vector3.Lerp(spikesActivePosition, spikesIdlePosition, moveTimer / transitionDuration);
            
            if (moveTimer >= transitionDuration)
            {
                isMovingDown = false;
                spikesMesh.SetActive(false); // Ẩn chông khi đã thụt xuống hết
            }
        }
    }

    // Hàm để gọi từ bên ngoài hoặc khi dẫm vào
    [ContextMenu("Activate Trap")]
    public void Activate()
    {
        if (isMovingUp || isMovingDown) return; // Nếu đang chạy thì không kích hoạt lại

        spikesMesh.SetActive(true);
        moveTimer = 0f;
        isMovingUp = true;
        if (activationEffect != null) activationEffect.Play();
    }

    void StartLowering()
    {
        moveTimer = 0f;
        isMovingDown = true;
    }

    // --- PHẦN GÂY SÁT THƯƠNG ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Nếu bẫy đang ẩn, thì kích hoạt bẫy
            if (!spikesMesh.activeSelf) 
            {
                Activate();
            }
            // Nếu bẫy đang nhô lên (hoặc đã lên cao), thì trừ máu
            else if (isMovingUp || Vector3.Distance(spikesMesh.transform.localPosition, spikesActivePosition) < 0.1f)
            {
                ApplyDamage();
            }
        }
    }

    void ApplyDamage()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.currentHealth -= 1;
            UIManager.Instance.healthUI.UpdateHealth(UIManager.Instance.currentHealth);

            if (UIManager.Instance.currentHealth <= 0)
                UIManagerEvent.Instance.LoseGame();
        }
    }
}