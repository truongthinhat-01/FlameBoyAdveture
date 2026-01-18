using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SkeletonController : MonoBehaviour, ISpawnable, IDamageable
{
    [Header("Refs")]
    public Animator animator;
    public NavMeshAgent agent;
    public Transform player;

    [Header("Stats")]
    public float detectRange = 15f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public int maxHP = 3;

    private int currentHP;
    private bool isDead;
    private bool isAttacking;
    private float attackTimer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // ❗ Chỉ TẮT di chuyển, KHÔNG tắt agent vĩnh viễn
        //agent.enabled = false;
        //gameObject.SetActive(false);
    }

    // 🔥 ĐƯỢC GỌI TỪ CoinManager / BossManager
    public void Spawn()
    {
        gameObject.SetActive(true);

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // 🔥 RESET NAVMESH (CỰC QUAN TRỌNG)
        agent.enabled = false;
        agent.enabled = true;

        agent.isStopped = false;
        agent.ResetPath();

        currentHP = maxHP;
        isDead = false;
        isAttacking = false;
        attackTimer = attackCooldown;

        animator.Rebind(); // reset toàn bộ state
        animator.Update(0f);
    }

    void Update()
    {
        if (isDead || isAttacking || player == null)
            return;

        if (!agent.enabled || !agent.isOnNavMesh)
            return;

        attackTimer += Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        // ATTACK
        if (distance <= attackRange && attackTimer >= attackCooldown)
        {
            StartCoroutine(AttackRoutine());
            return;
        }

        // CHASE
        if (distance <= detectRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        // Animator điều khiển Idle / Walk / Run
        animator.SetFloat("speed", agent.velocity.magnitude);
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        attackTimer = 0f;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        // Xoay mặt về player
        Vector3 look = player.position;
        look.y = transform.position.y;
        transform.LookAt(look);

        animator.SetTrigger("Attack");

        // ⏱ chờ đúng animation (ví dụ 1s)
        yield return new WaitForSeconds(1f);

        isAttacking = false;
        agent.isStopped = false;
    }

    // 🔥 BỊ ĐÁNH
    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        currentHP -= dmg;
        animator.SetTrigger("Hit");

        if (currentHP <= 0)
            Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        agent.isStopped = true;
        agent.enabled = false;

        animator.SetTrigger("Die");

        Destroy(gameObject, 3f);
    }
}
