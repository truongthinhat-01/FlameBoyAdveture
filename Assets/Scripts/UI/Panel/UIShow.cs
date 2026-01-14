using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShow : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text coinTxt;
    public Image[] hearts; // Kéo thả 3 icon trái tim vào đây trong Inspector
    public Button pauseButton;

    void Start()
    {
        // Gán sự kiện nút Pause
        if (pauseButton != null)
        {
            pauseButton.onClick.RemoveAllListeners(); // Xóa listener cũ tránh trùng lặp
            pauseButton.onClick.AddListener(OnPauseClick);
        }

        // Cập nhật Coin ban đầu
        // if (CoinManager.Instance != null)
        //     UpdateCoinUI(CoinManager.Instance.currentCoin);
            
        // Cập nhật Máu ban đầu
        // Sử dụng giá trị máu từ UIManager hoặc PlayerHealth tùy theo logic của bạn
        if (UIManager.HasInstance)
        {
            UpdateHealthUI(UIManager.Instance.currentHealth);
        }
    }

   public void UpdateCoinUI(int current)
{
    if (coinTxt == null) return;
    if (CoinManager.Instance == null) return;

    coinTxt.text = current + "/" + CoinManager.Instance.requiredCoin;
}


    // Hàm cập nhật trái tim (Đã sửa logic để cực kỳ ổn định)
    public void UpdateHealthUI(int hp)
    {
        if (hearts == null || hearts.Length == 0) return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                // Bật GameObject chứa trái tim nếu chỉ số i nhỏ hơn máu hiện tại
                hearts[i].gameObject.SetActive(i < hp);
            }
        }
    }

    public void OnPauseClick()
    {
        if (UIManagerEvent.HasInstance)
            UIManagerEvent.Instance.PauseGame();
    }
}