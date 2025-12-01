using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Animator anim;

    [Header("Settings")]
    public float walkRange = 8f;
    public float runRange = 4f;
    public float attackRange = 2f;

    public float walkSpeed = 1.5f;
    public float runSpeed = 3.5f;

    private bool isHitPlaying = false;   // Enemy đang trong animation Hit

    private void Update()
    {
        if (player == null) return;

        // Nếu đang bị trúng đạn → không làm gì nữa
        if (isHitPlaying) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > walkRange)
        {
            SetAnim(false, false, false);
            return;
        }

        // Xoay enemy về phía player
        Vector3 dir = (player.position - transform.position);
        dir.y = 0;
        transform.forward = dir.normalized;

        if (dist <= attackRange)
        {
            SetAnim(false, false, true);
            return;
        }

        if (dist <= runRange)
        {
            transform.position += transform.forward * runSpeed * Time.deltaTime;
            SetAnim(false, true, false);
            return;
        }

        transform.position += transform.forward * walkSpeed * Time.deltaTime;
        SetAnim(true, false, false);
    }

    // Hàm play animation Hit khi enemy trúng đạn
    //public void TakeDamage()
    //{
    //    if (isHitPlaying) return;

    //    isHitPlaying = true;
    //    anim.SetTrigger("isHit");
    //}
    public void TakeDamage()
    {
        if (isHitPlaying) return;

        isHitPlaying = true;
        anim.SetTrigger("isHit");
        StartCoroutine(ResetHit());
    }

    private IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.5f); // thời gian bằng độ dài animation Hit
        isHitPlaying = false;
        anim.ResetTrigger("isHit");
    }


    // Gọi từ Animation Event cuối animation Hit
    public void OnHitFinish()
    {
        isHitPlaying = false;
    }

    void SetAnim(bool walk, bool run, bool attack)
    {
        anim.SetBool("isWalk", walk);
        anim.SetBool("isRun", run);
        anim.SetBool("isAttack", attack);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
