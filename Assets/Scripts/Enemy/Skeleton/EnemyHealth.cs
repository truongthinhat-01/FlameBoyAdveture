using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHP = 100f;
    private float currentHP;

    public Image healthFill; // keo HealthFill vao day

    void Start()
    {
        currentHP = maxHP;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        UpdateHealthBar();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        healthFill.fillAmount = currentHP / maxHP;
    }

    void Die()
    {
        // TODO: goi animation chet
        Destroy(gameObject, 2f);
    }
}
