using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour
{
    public GameObject heartPrefab;

    List<Image> hearts = new List<Image>();

    public void Init(int maxHealth)
    {
        foreach (Transform c in transform)
            Destroy(c.gameObject);

        hearts.Clear();

        for (int i = 0; i < maxHealth; i++)
        {
            Image heart = Instantiate(heartPrefab, transform).GetComponent<Image>();
            hearts.Add(heart);
        }

        UpdateHealth(maxHealth);
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
            hearts[i].enabled = i < currentHealth;
    }
}
