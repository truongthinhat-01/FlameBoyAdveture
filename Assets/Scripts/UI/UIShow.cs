
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShow : MonoBehaviour
{
    public TMP_Text coinTxt;
    public Image[] hearts;
    public Button pauseButton;

    void Start()
    {
        if (pauseButton != null)
            pauseButton.onClick.AddListener(OnPauseClick);

        if (CoinManager.Instance != null)
            UpdateCoinUI(CoinManager.Instance.currentCoin);
    }

    public void UpdateCoinUI(int coin)
    {
        coinTxt.text = coin.ToString();
    }

    public void UpdateHealthUI(int hp)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < hp;
        }
    }

   public void OnPauseClick()
{
    if (UIManagerEvent.HasInstance)
        UIManagerEvent.Instance.PauseGame();
}

}
