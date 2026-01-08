
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController charCon;
    [SerializeField] private Animator anim;
    private bool isDead = false;
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

     CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        if (Camera.main != null) camTransform = Camera.main.transform;
        lastGrounded = true;
    }

    void Update()
    {
        if (isDead) return;

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
        moveAmount = Vector3.zero;
        yVelocity = 0f;
    }
}
