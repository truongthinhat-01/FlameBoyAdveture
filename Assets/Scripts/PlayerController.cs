using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController charCon;
    [SerializeField] private Animator amin;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce, gravityScale;
    [SerializeField] private float rotateSpeed = 10f;

    private CameraController cam;
    private Vector3 moveAmount;
    private float yStore;
    public GameObject JumpParticle, landingParticle;
    private bool lastGrondned;


    private void Start()
    {
        cam = FindAnyObjectByType<CameraController>();

        lastGrondned = true;

        charCon.Move(new Vector3(0f, Physics.gravity.y * gravityScale * Time.deltaTime, 0f));
    }


    private void FixedUpdate()
    {
        if (!charCon.isGrounded)
        {
            moveAmount.y = moveAmount.y + (Physics.gravity.y * gravityScale * Time.fixedDeltaTime);
        }
        else
        {
            moveAmount.y = Physics.gravity.y * gravityScale * Time.deltaTime;
        }

    }


    void Update()
    {
        yStore = moveAmount.y;
        moveAmount = (cam.transform.forward * Input.GetAxisRaw("Vertical")) + (cam.transform.right * Input.GetAxisRaw("Horizontal"));
        moveAmount.y = 0f;
        moveAmount = moveAmount.normalized;


        if (moveAmount.magnitude > .1f)
        {

            if (moveAmount != Vector3.zero)
            {
                Quaternion newRo = Quaternion.LookRotation(moveAmount);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRo, rotateSpeed * Time.deltaTime);
            }
        }

        moveAmount.y = yStore;


        if (charCon.isGrounded)
        {
            JumpParticle.SetActive(false);

            if (!lastGrondned)
            {
                landingParticle.SetActive(true);
            }

            if (Input.GetButtonDown("Jump"))
            {
                moveAmount.y = jumpForce;
                JumpParticle.SetActive(true);
            }
        }
        lastGrondned = charCon.isGrounded;

        charCon.Move(new Vector3(moveAmount.x * moveSpeed, moveAmount.y, moveAmount.z * moveSpeed) * Time.deltaTime);

        float moveVel = new Vector3(moveAmount.x, 0f, moveAmount.z).magnitude * moveSpeed;

        amin.SetFloat("speed", moveVel);
        amin.SetBool("isGrounded", charCon.isGrounded);
        amin.SetFloat("yVel", moveAmount.y);


    }
}
