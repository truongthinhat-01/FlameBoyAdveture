// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     [SerializeField] private CharacterController charCon;
//     [SerializeField] private Animator amin;
//     [SerializeField] private float moveSpeed;
//     [SerializeField] private float jumpForce, gravityScale;
//     [SerializeField] private float rotateSpeed = 10f;

//     private CameraController cam;
//     private Vector3 moveAmount;
//     private float yStore;
//     public GameObject jumpParticle, landingParticle, skillParticleOne,skillParticleTwo;
//     private bool lastGrondned;
//     public Transform pawPointOne,pawPointTwo;



//     private void Start()
//     {
//         cam = FindAnyObjectByType<CameraController>();

//         lastGrondned = true;

//         charCon.Move(new Vector3(0f, Physics.gravity.y * gravityScale * Time.deltaTime, 0f));
//     }


//     private void FixedUpdate()
//     {
//         if (!charCon.isGrounded)
//         {
//             moveAmount.y = moveAmount.y + (Physics.gravity.y * gravityScale * Time.fixedDeltaTime);
//         }
//         else
//         {
//             moveAmount.y = Physics.gravity.y * gravityScale * Time.deltaTime;
//         }

//     }
//     void Update()
// {
//     // ❗ NẾU ĐÃ CHẾT → KHÔNG ĐƯỢC CẬP NHẬT ANIMATOR NỮA
//     if (amin.GetBool("isDead"))
//         return;

//     yStore = moveAmount.y;
//     moveAmount = (cam.transform.forward * Input.GetAxisRaw("Vertical")) +
//                  (cam.transform.right * Input.GetAxisRaw("Horizontal"));
//     moveAmount.y = 0f;
//     moveAmount = moveAmount.normalized;

//     if (moveAmount.magnitude > .1f)
//     {
//         Quaternion newRo = Quaternion.LookRotation(moveAmount);
//         transform.rotation = Quaternion.Slerp(transform.rotation, newRo, rotateSpeed * Time.deltaTime);
//     }

//     moveAmount.y = yStore;

//     if (charCon.isGrounded)
//     {
//         jumpParticle.SetActive(false);

//         if (!lastGrondned)
//             landingParticle.SetActive(true);

//         if (Input.GetButtonDown("Jump"))
//         {
//             moveAmount.y = jumpForce;
//             jumpParticle.SetActive(true);
//         }
//     }

//     lastGrondned = charCon.isGrounded;

//     charCon.Move(new Vector3(
//         moveAmount.x * moveSpeed,
//         moveAmount.y,
//         moveAmount.z * moveSpeed
//     ) * Time.deltaTime);

//     float moveVel = new Vector3(moveAmount.x, 0f, moveAmount.z).magnitude * moveSpeed;

//     amin.SetFloat("speed", moveVel);
//     amin.SetBool("isGrounded", charCon.isGrounded);
//     amin.SetFloat("yVel", moveAmount.y);

//      if (Input.GetKeyDown(KeyCode.Alpha2) && !amin.GetCurrentAnimatorStateInfo(0).IsName("SkillTwo"))
//         {

//             amin.SetTrigger("skillTwo");
//             Instantiate(skillParticleTwo, pawPointTwo.position, pawPointTwo.rotation);

//         }

//    if (Input.GetKeyDown(KeyCode.Alpha2) && !amin.GetCurrentAnimatorStateInfo(0).IsName("SkillTwo"))
//         {

//             amin.SetTrigger("skillTwo");
//             Instantiate(skillParticleTwo, pawPointTwo.position, pawPointTwo.rotation);

//         }


//     }
//     // void Update()
//     // {
//     //     yStore = moveAmount.y;
//     //     moveAmount = (cam.transform.forward * Input.GetAxisRaw("Vertical")) + (cam.transform.right * Input.GetAxisRaw("Horizontal"));
//     //     moveAmount.y = 0f;
//     //     moveAmount = moveAmount.normalized;


//     //     if (moveAmount.magnitude > .1f)
//     //     {

//     //         if (moveAmount != Vector3.zero)
//     //         {
//     //             Quaternion newRo = Quaternion.LookRotation(moveAmount);
//     //             transform.rotation = Quaternion.Slerp(transform.rotation, newRo, rotateSpeed * Time.deltaTime);
//     //         }
//     //     }

//     //     moveAmount.y = yStore;


//     //     if (charCon.isGrounded)
//     //     {
//     //         jumpParticle.SetActive(false);

//     //         if (!lastGrondned)
//     //         {
//     //             landingParticle.SetActive(true);
//     //         }

//     //         if (Input.GetButtonDown("Jump"))
//     //         {
//     //             moveAmount.y = jumpForce;
//     //             jumpParticle.SetActive(true);
//     //         }
//     //     }
//     //     lastGrondned = charCon.isGrounded;

//     //     charCon.Move(new Vector3(moveAmount.x * moveSpeed, moveAmount.y, moveAmount.z * moveSpeed) * Time.deltaTime);

//     //     float moveVel = new Vector3(moveAmount.x, 0f, moveAmount.z).magnitude * moveSpeed;

//     //     amin.SetFloat("speed", moveVel);
//     //     amin.SetBool("isGrounded", charCon.isGrounded);
//     //     amin.SetFloat("yVel", moveAmount.y);

//     //     if (Input.GetKeyDown(KeyCode.Alpha1) && !amin.GetCurrentAnimatorStateInfo(0).IsName("SkillOne"))
//     //     {
            
//     //         amin.SetTrigger("skillOne");
//     //         Instantiate(skillParticleOne, pawPointOne.position, pawPointOne.rotation);
           
//     //     }
//     //     if (Input.GetKeyDown(KeyCode.Alpha2) && !amin.GetCurrentAnimatorStateInfo(0).IsName("SkillTwo"))
//     //     {

//     //         amin.SetTrigger("skillTwo");
//     //         Instantiate(skillParticleTwo, pawPointTwo.position, pawPointTwo.rotation);

//     //     }


//     // }

// }






using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController charCon;
    [SerializeField] private Animator anim;
    private CameraController cam;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravityScale = 2f;
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
        cam = FindAnyObjectByType<CameraController>();
        lastGrounded = true;
    }

    void Update()
    {
        
        // ❌ Nếu chết → khóa toàn bộ điều khiển
        if (anim.GetBool("isDead"))
            return;

        HandleMovement();
        HandleJumpAndGravity();
        HandleSkills();
        UpdateAnimator();
    }

    // ================= MOVEMENT =================
    void HandleMovement()
    {
        Vector3 input = (cam.transform.forward * Input.GetAxisRaw("Vertical")) +
                        (cam.transform.right * Input.GetAxisRaw("Horizontal"));
        input.y = 0f;
        input.Normalize();

        if (input.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(input);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }

        moveAmount.x = input.x * moveSpeed;
        moveAmount.z = input.z * moveSpeed;
    }

    // ================= JUMP + GRAVITY =================
    void HandleJumpAndGravity()
    {
        if (charCon.isGrounded)
        {
            if (!lastGrounded && landingParticle)
                landingParticle.SetActive(true);

            if (yVelocity < 0)
                yVelocity = -2f; // ép dính đất

            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpForce;
                if (jumpParticle) jumpParticle.SetActive(true);
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

    // ================= SKILLS =================
    void HandleSkills()
    {
        // Skill 1
        if (Input.GetKeyDown(KeyCode.Alpha1) &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("SkillOne"))
        {
            anim.SetTrigger("skillOne");
            if (skillParticleOne && pawPointOne)
                Instantiate(skillParticleOne, pawPointOne.position, pawPointOne.rotation);
        }

        // Skill 2
        if (Input.GetKeyDown(KeyCode.Alpha2) &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("SkillTwo"))
        {
            anim.SetTrigger("skillTwo");
            if (skillParticleTwo && pawPointTwo)
                Instantiate(skillParticleTwo, pawPointTwo.position, pawPointTwo.rotation);
        }
    }

    // ================= ANIMATOR =================
    void UpdateAnimator()
    {
        Vector3 horizontalVel = new Vector3(moveAmount.x, 0f, moveAmount.z);

        anim.SetFloat("speed", horizontalVel.magnitude);
        anim.SetBool("isGrounded", charCon.isGrounded);
        anim.SetFloat("yVel", yVelocity);
    }

    // ================= PUBLIC =================
    public void Die()
    {
        anim.SetBool("isDead", true);
        moveAmount = Vector3.zero;
        yVelocity = 0f;
    }
}

