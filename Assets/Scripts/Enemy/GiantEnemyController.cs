using UnityEngine;
using System.Collections;

public class GiantEnemyController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Animator anim;
    public StairController stair;

    [Header("Settings")]
    public float walkRange = 8f;
    public float runRange = 4f;
    public float attackRange = 2f;

    public float walkSpeed = 1.5f;
    public float runSpeed = 3.5f;

    private bool isHitPlaying = false;
    private bool isDeath = false;

    public int maxHit = 3;
    private int currentHit = 0;

    private void OnEnable()
    {
        // Reset lại trạng thái khi vừa bật từ inactive
        isHitPlaying = false;
        isDeath = false;
        currentHit = 0;

        if (anim != null)
        {
            anim.Rebind();       // Reset animator
            anim.Update(0f);     // Áp dụng ngay
        }
    }

    private void Update()
    {
        if (player == null || isDeath) return;
        if (isHitPlaying) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > walkRange)
        {
            SetAnim(false, false, false);
            return;
        }

        // Xoay hướng
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

    public void TakeDamage()
    {
        if (isHitPlaying || isDeath) return;

        currentHit++;

        if (currentHit >= maxHit)
        {
            Die();
            return;
        }

        isHitPlaying = true;
        anim.SetTrigger("isHit");
        StartCoroutine(ResetHit());
    }

    private IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.5f);
        isHitPlaying = false;
        anim.ResetTrigger("isHit");
    }

    void Die()
    {
        isDeath = true;

        anim.SetTrigger("isDeath");

        if (TryGetComponent(out Collider col))
            col.enabled = false;

        anim.SetBool("isWalk", false);
        anim.SetBool("isRun", false);
        anim.SetBool("isAttack", false);
        stair.OnPlayerComplete();

        Destroy(gameObject, 3f);
    }

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
}
