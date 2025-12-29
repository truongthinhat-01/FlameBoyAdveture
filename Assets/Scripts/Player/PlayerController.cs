// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     [Header("References")]
//     [SerializeField] private CharacterController charCon;
//     [SerializeField] private Animator anim;
//     private CameraController cam;

//     [Header("Movement")]
//     [SerializeField] private float moveSpeed = 5f;
//     [SerializeField] private float jumpForce = 8f;
//     [SerializeField] private float gravityScale = 2f;
//     [SerializeField] private float rotateSpeed = 10f;

//     [Header("Particles")]
//     public GameObject jumpParticle;
//     public GameObject landingParticle;
//     public GameObject skillParticleOne;
//     public GameObject skillParticleTwo;

//     [Header("Skill Points")]
//     public Transform pawPointOne;
//     public Transform pawPointTwo;

//     private Vector3 moveAmount;
//     private float yVelocity;
//     private bool lastGrounded;

//     void Start()
//     {
//         cam = FindAnyObjectByType<CameraController>();
//         lastGrounded = true;
//     }

//     void Update()
//     {
        
//         // ❌ Nếu chết → khóa toàn bộ điều khiển
//         if (anim.GetBool("isDead"))
//             return;

//         HandleMovement();
//         HandleJumpAndGravity();
//         HandleSkills();
//         UpdateAnimator();
//     }

//     // ================= MOVEMENT =================
//     void HandleMovement()
//     {
//         Vector3 input = (cam.transform.forward * Input.GetAxisRaw("Vertical")) +
//                         (cam.transform.right * Input.GetAxisRaw("Horizontal"));
//         input.y = 0f;
//         input.Normalize();

//         if (input.magnitude > 0.1f)
//         {
//             Quaternion targetRot = Quaternion.LookRotation(input);
//             transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
//         }

//         moveAmount.x = input.x * moveSpeed;
//         moveAmount.z = input.z * moveSpeed;
//     }

//     // ================= JUMP + GRAVITY =================
//     void HandleJumpAndGravity()
//     {
//         if (charCon.isGrounded)
//         {
//             if (!lastGrounded && landingParticle)
//                 landingParticle.SetActive(true);

//             if (yVelocity < 0)
//                 yVelocity = -2f; // ép dính đất

//             if (Input.GetButtonDown("Jump"))
//             {
//                 yVelocity = jumpForce;
//                 if (jumpParticle) jumpParticle.SetActive(true);
//             }
//         }
//         else
//         {
//             yVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;
//         }

//         lastGrounded = charCon.isGrounded;

//         moveAmount.y = yVelocity;
//         charCon.Move(moveAmount * Time.deltaTime);
//     }

//     // ================= SKILLS =================
//     void HandleSkills()
//     {
//         // Skill 1
//         if (Input.GetKeyDown(KeyCode.Alpha1) &&
//             !anim.GetCurrentAnimatorStateInfo(0).IsName("SkillOne"))
//         {
//             anim.SetTrigger("skillOne");
//             if (skillParticleOne && pawPointOne)
//                 Instantiate(skillParticleOne, pawPointOne.position, pawPointOne.rotation);
//         }

//         // Skill 2
//         if (Input.GetKeyDown(KeyCode.Alpha2) &&
//             !anim.GetCurrentAnimatorStateInfo(0).IsName("SkillTwo"))
//         {
//             anim.SetTrigger("skillTwo");
//             if (skillParticleTwo && pawPointTwo)
//                 Instantiate(skillParticleTwo, pawPointTwo.position, pawPointTwo.rotation);
//         }
//     }

//     // ================= ANIMATOR =================
//     void UpdateAnimator()
//     {
//         Vector3 horizontalVel = new Vector3(moveAmount.x, 0f, moveAmount.z);

//         anim.SetFloat("speed", horizontalVel.magnitude);
//         anim.SetBool("isGrounded", charCon.isGrounded);
//         anim.SetFloat("yVel", yVelocity);
//     }

//     // ================= PUBLIC =================
//     public void Die()
//     {
//         anim.SetBool("isDead", true);
//         moveAmount = Vector3.zero;
//         yVelocity = 0f;
//     }
// }

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController charCon;
    [SerializeField] private Animator anim;
    private Transform camTransform; // Lưu cache transform camera

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravityScale = 2.5f; // Tăng một chút cho cảm giác nhảy đầm hơn
    [SerializeField] private float rotateSpeed = 10f;

    [Header("Particles")]
    public GameObject jumpParticle;
    public GameObject landingParticle;
    public GameObject skillParticleOne;
    public GameObject skillParticleTwo;

    [Header("Skill Points")]
    public Transform pawPointOne;
    public Transform pawPointTwo;

    private Vector3 moveAmount;
    private float yVelocity;
    private bool lastGrounded;

    void Start()
    {
        if (Camera.main != null) camTransform = Camera.main.transform;
        lastGrounded = true;
    }

    void Update()
    {
        if (anim.GetBool("isDead")) return;

        HandleMovement();
        HandleJumpAndGravity();
        HandleSkills();
        UpdateAnimator();
    }

    void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Tính hướng dựa trên Camera
        Vector3 forward = camTransform.forward;
        Vector3 right = camTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = (forward * v + right * h).normalized;

        if (moveDir.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }

        moveAmount.x = moveDir.x * moveSpeed;
        moveAmount.z = moveDir.z * moveSpeed;
    }

    void HandleJumpAndGravity()
    {
        if (charCon.isGrounded)
        {
            // Hiệu ứng hạ cánh
            if (!lastGrounded)
            {
                SpawnParticle(landingParticle, transform.position);
            }

            if (yVelocity < 0) yVelocity = -2f; 

            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpForce;
                anim.SetTrigger("jump"); // Nên có trigger riêng cho jump
                SpawnParticle(jumpParticle, transform.position);
            }
        }
        else
        {
            yVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        lastGrounded = charCon.isGrounded;
        moveAmount.y = yVelocity;
        charCon.Move(moveAmount * Time.deltaTime);
    }

    void HandleSkills()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !IsPlayingAnimation("SkillOne"))
        {
            anim.SetTrigger("skillOne");
            SpawnParticle(skillParticleOne, pawPointOne.position, pawPointOne.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !IsPlayingAnimation("SkillTwo"))
        {
            anim.SetTrigger("skillTwo");
            SpawnParticle(skillParticleTwo, pawPointTwo.position, pawPointTwo.rotation);
        }
    }

    // Hàm phụ trợ để tránh lặp code Instantiate
    void SpawnParticle(GameObject prefab, Vector3 pos, Quaternion rot = default)
{
    if (prefab) 
    {
        GameObject newParticle = Instantiate(prefab, pos, rot);
        // Tự động xóa sau 2 giây (hoặc tùy độ dài của hiệu ứng)
        Destroy(newParticle, 1.5f); 
    }
}

    bool IsPlayingAnimation(string stateName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    void UpdateAnimator()
    {
        Vector2 horizontalVel = new Vector2(moveAmount.x, moveAmount.z);
        anim.SetFloat("speed", horizontalVel.magnitude);
        anim.SetBool("isGrounded", charCon.isGrounded);
        anim.SetFloat("yVel", yVelocity);
    }

    public void Die()
    {
        anim.SetBool("isDead", true);
        // Ngừng di chuyển ngay lập tức
        charCon.Move(Vector3.zero);
    }
}
