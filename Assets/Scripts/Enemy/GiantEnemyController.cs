using UnityEngine;
using System.Collections;

public class GiantEnemyController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Animator anim;
    public StairController stair;

    [Header("Colliders")]
    public Collider bodyCollider; 
    public Collider handCollider; // Collider này sẽ được bật/tắt bởi Animation Event

    [Header("Settings")]
    public float walkRange = 10f;
    public float runRange = 6f;
    public float attackRange = 2.5f;
    public float walkSpeed = 1.5f;
    public float runSpeed = 3.5f;

    private bool isHitPlaying = false;
    private bool isDeath = false;
    private int currentHit = 0;
    public int maxHit = 3;

    private void Start() {
        if (handCollider != null) handCollider.enabled = false; // Mặc định tắt tay
    }

    private void Update()
    {
        if (player == null || isDeath || isHitPlaying) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // 1. Logic xoay mặt về phía player (chỉ khi trong tầm nhìn)
        if (dist <= walkRange) {
            Vector3 dir = (player.position - transform.position);
            dir.y = 0;
            if (dir != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }

        // 2. Logic di chuyển và Animation
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

    // Hàm này giúp chống "loạn" bằng cách kiểm tra giá trị cũ trước khi Set
    void UpdateAnimationStates(bool walk, bool run, bool attack) {
        if (anim == null) return;
        if (anim.GetBool("isWalk") != walk) anim.SetBool("isWalk", walk);
        if (anim.GetBool("isRun") != run) anim.SetBool("isRun", run);
        if (anim.GetBool("isAttack") != attack) anim.SetBool("isAttack", attack);
    }

    // --- Animation Events (Gọi từ cửa sổ Animation của đòn đánh) ---
    public void EnableHandCollider() {
        if (handCollider != null) handCollider.enabled = true;
    }

    public void DisableHandCollider() {
        if (handCollider != null) handCollider.enabled = false;
    }

    public void TakeDamage() {
        if (isHitPlaying || isDeath) return;
        currentHit++;
        if (currentHit >= maxHit) { Die(); return; }
        
        isHitPlaying = true;
        anim.SetTrigger("isHit");
        StartCoroutine(ResetHitStatus());
    }

    private IEnumerator ResetHitStatus() {
        yield return new WaitForSeconds(0.8f); // Thời gian chờ khớp với clip Hit
        isHitPlaying = false;
    }

    void Die() {
        isDeath = true;
        anim.SetTrigger("isDeath");
        if (bodyCollider) bodyCollider.enabled = false;
        if (handCollider) handCollider.enabled = false;
        if (stair != null) stair.OnPlayerComplete();
        Destroy(gameObject, 4f);
    }
}