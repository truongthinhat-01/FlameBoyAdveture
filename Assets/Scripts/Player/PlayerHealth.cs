using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 3;
    public int currentHealth;

    [Header("Hit Effect - Particle")]
    public GameObject hitParticle;
    public Transform pawHit;

    [Header("Hit Flash Colors")]
    public Color hitFlashColor = Color.red;
    public Color whiteFlashColor = Color.white;

    [Header("Blink Setting")]
    public int blinkCount = 3;
    public float blinkInterval = 0.07f;

    Animator animator;
    bool isDead;
    bool isInvulnerable;

    Renderer[] renderers;
    Color[] originalColors;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
            Debug.LogError("❌ Animator NOT FOUND");

        if (pawHit == null)
            pawHit = transform;

        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            Material mat = renderers[i].material;

            if (mat.HasProperty("_BaseColor"))
                originalColors[i] = mat.GetColor("_BaseColor");
            else if (mat.HasProperty("_Color"))
                originalColors[i] = mat.color;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;

        if (UIManager.HasInstance)
            UIManager.Instance.UpdatePlayerHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvulnerable) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (UIManager.HasInstance)
            UIManager.Instance.UpdatePlayerHealth(currentHealth);

        PlayHitParticle();
        StartCoroutine(HitFlashRoutine());

        if (currentHealth <= 0)
            StartCoroutine(DieRoutine());
        else
            StartCoroutine(HitRoutine());
    }

    // ================= HIT =================
    IEnumerator HitRoutine()
    {
        isInvulnerable = true;

        animator.ResetTrigger("Die");
        animator.SetTrigger("Hit");

        yield return new WaitForSeconds(0.8f);
        isInvulnerable = false;
    }

    // ================= FLASH (RED → WHITE) =================
    IEnumerator HitFlashRoutine()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            SetPlayerColor(hitFlashColor);   // 🔴 ĐỎ
            yield return new WaitForSeconds(blinkInterval);

            SetPlayerColor(whiteFlashColor); // ⚪ TRẮNG
            yield return new WaitForSeconds(blinkInterval);
        }

        RestoreOriginalColor(); // 🎨 Trả về màu gốc
    }

    void SetPlayerColor(Color color)
    {
        foreach (Renderer r in renderers)
        {
            Material mat = r.material;

            if (mat.HasProperty("_BaseColor"))
                mat.SetColor("_BaseColor", color);
            else if (mat.HasProperty("_Color"))
                mat.color = color;
        }
    }

    void RestoreOriginalColor()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            Material mat = renderers[i].material;

            if (mat.HasProperty("_BaseColor"))
                mat.SetColor("_BaseColor", originalColors[i]);
            else if (mat.HasProperty("_Color"))
                mat.color = originalColors[i];
        }
    }

    // ================= DIE =================
    IEnumerator DieRoutine()
    {
        if (isDead) yield break;
        isDead = true;

        animator.ResetTrigger("Hit");
        animator.SetTrigger("Die");

        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null) pc.Die();

        yield return new WaitForSeconds(2.5f);

        if (UIManager.HasInstance)
            UIManager.Instance.ShowLose();
    }

    void PlayHitParticle()
    {
        if (hitParticle == null) return;

        Instantiate(
            hitParticle,
            pawHit.position,
            Quaternion.identity
        );
    }
    public void HealFull()
{
    if (isDead) return;

    currentHealth = maxHealth;

    if (UIManager.HasInstance)
        UIManager.Instance.UpdatePlayerHealth(currentHealth);
}

}
