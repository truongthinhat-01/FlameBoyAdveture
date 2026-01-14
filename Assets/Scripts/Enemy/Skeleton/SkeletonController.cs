using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;

public class SkeletonController : MonoBehaviour, IDamageable

{
    [Header("Components")]
    public Animator animator;
    public NavMeshAgent agent;
    public Transform player;

    public Collider bobySke;
    public Collider handSke;

    [Header("UI")]
    public Image healthFill;

    [Header("Stats")]
    public float detectRange = 15f;
    public float attackRange = 2f;
    public float attackCooldown = 3.0f; // Tổng thời gian đứng im (chém + nghỉ)

    [Header("Event khi chết")]
    public ColliderMoveDownY rock;

    private bool isDead = false;
    private bool isAttacking = false;
    private bool hasSpawned = false;
    private bool isHitPlay = false;
    public int maxHit = 5; 
    
    private int currentHit = 0;

    void Awake()
{
    animator = GetComponent<Animator>();
    agent = GetComponent<NavMeshAgent>();

    if (player == null)
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

    agent.enabled = false;          // ❗ Tắt agent trước
    gameObject.SetActive(false);    // Ẩn enemy chờ trigger
}

//     public void SpawnEnemy()
// {
//     gameObject.SetActive(true);
//     hasSpawned = true;

//     agent.enabled = true;       // BẬT NGAY
//     agent.isStopped = true;     // Đứng yên frame đầu

//     animator.Play("Idle");      // hoặc để default
// }
// ➕ THÊM: cập nhật thanh máu theo hit
void UpdateHealthBar()
{
    if (healthFill != null)
        healthFill.fillAmount = Mathf.Clamp01(
            1f - (float)currentHit / maxHit
        );
}



public void SpawnEnemy()
{
    gameObject.SetActive(true);
    hasSpawned = true;

    agent.enabled = true;
    agent.isStopped = true;

    currentHit = 0;        // 🔧 SỬA: reset số hit
    UpdateHealthBar();     // ➕ THÊM: reset thanh máu

    animator.Play("Idle");
}

    void Update()
    {
        if (!hasSpawned || isDead || agent == null || !agent.enabled || !agent.isOnNavMesh) return;

        // Nếu đang chém hoặc đang nghỉ thì KHÔNG chạy logic đuổi theo
        if (isAttacking) return;

        float distance = Vector3.Distance(transform.position, player.position);
        float speed = agent.velocity.magnitude;

        // Đẩy giá trị speed vào Animator để tự chuyển Idle/Walk/Run (đã tách rời)
        animator.SetFloat("speed", speed);

        // if (distance <= attackRange)
        // {
        //     StartCoroutine(AttackRoutine());
        // }

        if (distance <= attackRange && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }

        else if (distance <= detectRange)
        {
            MoveToPlayer();
        }
        else
        {
            StopMoving();
        }
    }

    void MoveToPlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    void StopMoving()
    {
        if (agent.isActiveAndEnabled)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero; // Triệt tiêu lực quán tính để không bị trượt
        }
    }

IEnumerator AttackRoutine()
{
    isAttacking = true;

    // 1. NGẮT LỰC: Xóa mọi dấu vết di chuyển cũ
    agent.ResetPath(); 
    agent.velocity = Vector3.zero;
    agent.isStopped = true;

    // 2. KHÓA ANIMATOR: Xóa các trigger cũ để tránh chém bồi
    animator.ResetTrigger("isAttack"); 
    animator.SetFloat("speed", 0); // Ép về Idle ngay

    // 3. XOAY MẶT: Nhìn thẳng Player trước khi vung kiếm
    Vector3 lookPos = player.position;
    lookPos.y = transform.position.y;
    transform.LookAt(lookPos);

    // 4. CHÉM
    animator.SetTrigger("isAttack");

    // 5. NGHỈ TUYỆT ĐỐI: Đợi hết 3 giây cooldown
    // Trong thời gian này, hàm Update sẽ không thể gọi thêm bất cứ lệnh nào
    yield return new WaitForSeconds(attackCooldown);

    // 6. DỌN DẸP: Xóa trigger lần nữa trước khi mở khóa
    animator.ResetTrigger("isAttack"); 
    
    isAttacking = false;
    // Sau dòng này, Skeleton mới bắt đầu "tỉnh dậy" để đuổi tiếp
}
    // public void TakeDamage(int damage)
    // {
    //     if (isHitPlay || isDead) return;
    //     currentHit ++;
    //     if(currentHit >=maxHit){ Die();return;}
    //     isHitPlay = true;
    //     animator.SetTrigger("isHit");
    //      StartCoroutine(ResetHitStatus());
    // }
     
    //  private IEnumerator ResetHitStatus() {
    //     yield return new WaitForSeconds(0.8f); // Thời gian chờ khớp với clip Hit
    //     isHitPlay = false;
    // }

    // ➕ BẮT BUỘC PHẢI CÓ
private IEnumerator ResetHitStatus()
{
    yield return new WaitForSeconds(0.8f); // khớp animation Hit
    isHitPlay = false;
}


    public void TakeDamage(int damage)
{
    if (isDead || isHitPlay) return;

    currentHit += damage;   // 🔧 SỬA: cộng hit
    UpdateHealthBar();      // ➕ THÊM: update UI

    if (currentHit >= maxHit)
    {
        Die();              // 🔧 SỬA: chết theo số hit
        return;
    }

    isHitPlay = true;
    animator.SetTrigger("isHit");
    StartCoroutine(ResetHitStatus());
}

    public void Die()
{
    if (isDead) return;
    isDead = true;
    
    if (healthFill != null)
    healthFill.transform.parent.gameObject.SetActive(false);


    StopMoving();

    if (agent != null)
        agent.enabled = false; // không cản player

    if (animator != null)
        animator.SetTrigger("Die");

    if (bobySke != null)
        bobySke.enabled = false;

    if (handSke != null)
        handSke.enabled = false;

    if (rock != null)
        rock.StartMoveDown();

    Destroy(gameObject, 3f); // chỉ 1 lần
}

    public void EnableHandCollider()
    {
    if (handSke != null)
        handSke.enabled = true;
    }

    public void DisableHandCollider()
    {
        if (handSke != null)
            handSke.enabled = false;
    }

}