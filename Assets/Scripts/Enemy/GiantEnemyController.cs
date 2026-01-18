using UnityEngine;
using System.Collections;

public class GiantEnemyController : MonoBehaviour, IDamageable
{

    [Header("References")]
    public Transform player;
    public Animator anim;

    [Header("Colliders")]
    public Collider bodyCollider;
    public Collider handCollider;

    [Header("Ranges")]
    public float walkRange = 12f;
    public float runRange = 7f;
    public float attackRange = 2.5f;

    [Header("Speed")]
    public float walkSpeed = 1.5f;
    public float runSpeed = 3.5f;

    [Header("Stats")]
    public int maxHit = 3;

    bool isDead;
    bool isHit;
    int currentHit;
    bool isAttacking;


    // =========================
    void Start()
    {
        //if (handCollider) handCollider.enabled = false;

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p) player = p.transform;
        }
    }

void Update()
{
    if (player == null || isDead || isHit) return;

    float dist = Vector3.Distance(transform.position, player.position);

    // 🔒 ĐANG ATTACK → ĐỨNG IM
    if (isAttacking)
    {
        SetState(false, false, true);
        return;
    }

    // Xoay mặt về player (chỉ khi KHÔNG attack)
    Vector3 dir = player.position - transform.position;
    dir.y = 0;
    if (dir != Vector3.zero)
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(dir),
            Time.deltaTime * 5f
        );
    }

    if (dist <= attackRange)
    {
        //isAttacking = true;
        SetState(false, false, true);
    }
    else if (dist <= runRange)
    {
        transform.position += transform.forward * runSpeed * Time.deltaTime;
        SetState(false, true, false);
    }
    else if (dist <= walkRange)
    {
        transform.position += transform.forward * walkSpeed * Time.deltaTime;
        SetState(true, false, false);
    }
    else
    {
        SetState(false, false, false);
    }
}
void SetState(bool walk, bool run, bool attack)
{
    anim.SetBool("isWalk", walk);
    anim.SetBool("isRun", run);
    anim.SetBool("isAttack", attack);
}


    // =========================
    void ResetAnim()
    {
        anim.SetBool("isWalk", false);
        anim.SetBool("isRun", false);
        anim.SetBool("isAttack", false);
    }

    void Move(float speed)
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void LookAtPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 6f);
        }
    }

    // =========================
    public void TakeDamage(int dmg)
    {
        if (isDead || isHit) return;

        currentHit += dmg;

        if (currentHit >= maxHit)
        {
            Die();
            return;
        }

        isHit = true;
        ResetAnim();
        anim.SetTrigger("isHit");
        StartCoroutine(EndHit());
    }

    IEnumerator EndHit()
    {
        yield return new WaitForSeconds(0.8f);
        isHit = false;
    }

    // =========================
    void Die()
    {
        isDead = true;
        ResetAnim();
        anim.SetTrigger("isDeath");

        if (bodyCollider) bodyCollider.enabled = false;
        if (handCollider) handCollider.enabled = false;

        Destroy(gameObject, 4f);
    }

    // =========================
    // ANIMATION EVENTS
    public void EnableHandCollider()
    {
        if (handCollider) handCollider.enabled = true;
    }

    public void DisableHandCollider()
    {
        if (handCollider) handCollider.enabled = false;
    }
    public void StartAttack()
{
    isAttacking = true;
}
public void EndAttack()
{
    isAttacking = false;
}


}
