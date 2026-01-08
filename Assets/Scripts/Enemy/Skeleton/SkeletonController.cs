using UnityEngine;
using System.Collections;

public class SkeletonController : MonoBehaviour
{
    public Transform player;
    public Animator anim;
    public Collider bodyCollider;
    public Collider handCollider;

    public float walkRange = 15f;
    public float runRange = 8f;
    public float attackRange = 2.5f;
    public float walkSpeed = 1.5f;
    public float runSpeed = 3.5f;

    public bool isAwake = false;
    private bool isHitPlaying = false;
    private bool isDeath = false;
    private int currentHit = 0;
    public int maxHit = 3;

    private void Start() {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        if (handCollider != null) 
            handCollider.enabled = false;
    }

    private void Update()
    {
        if (!isAwake) return;
        if (player == null || isDeath || isHitPlaying) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // Rotate
        if (dist <= walkRange) {
            Vector3 dir = player.position - transform.position;
            dir.y = 0;
            if (dir != Vector3.zero) {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(dir),
                    Time.deltaTime * 5f
                );
            }
        }

        // Movement
        if (dist <= attackRange) {
            UpdateAnimationStates(false, false, true);
        }
        else if (dist <= runRange) {
            transform.position += transform.forward * runSpeed * Time.deltaTime;
            UpdateAnimationStates(false, true, false);
        }
        else if (dist <= walkRange) {
            transform.position += transform.forward * walkSpeed * Time.deltaTime;
            UpdateAnimationStates(true, false, false);
        }
        else {
            UpdateAnimationStates(false, false, false);
        }
    }

    public void WakeUp() {
        isAwake = true;
        Debug.Log("BOSS THỨC!!");
    }

    void UpdateAnimationStates(bool walk, bool run, bool attack) {
        anim.SetBool("isWalk", walk);
        anim.SetBool("isRun", run);
        anim.SetBool("isAttack", attack);
    }
}
