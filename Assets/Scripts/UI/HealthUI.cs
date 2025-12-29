using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour
{
    public GameObject heartPrefab; // Kéo Prefab trái tim có component Image vào đây
    private List<Image> hearts = new List<Image>();

    public void Init(int maxHealth)
    {
        foreach (Transform c in transform)
            Destroy(c.gameObject);

        hearts.Clear();

        // Tạo mới các trái tim theo maxHealth
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heartObj = Instantiate(heartPrefab, transform);
            Image heartImg = heartObj.GetComponent<Image>();
            
            if (heartImg != null)
            {
                hearts.Add(heartImg);
            }
        }
        if (UIManager.HasInstance)
        {
            UpdateHealth(UIManager.Instance.currentHealth);
        }
    }

    // Hàm cập nhật ẩn/hiện trái tim
    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (hearts[i] != null)
            {
  
                hearts[i].gameObject.SetActive(i < currentHealth);
            }
        }
        Debug.Log("HealthUI: Đã cập nhật còn " + currentHealth + " trái tim.");
    }
}