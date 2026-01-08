using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
     public static CountdownTimer Instance;

    public float startTime = 120f;
    private float currentTime;
    private bool isRuning;

    public TextMeshProUGUI timeText;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       currentTime = startTime;

       isRuning = true;
       UpdateTimeText();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!isRuning)return;
        currentTime -=Time.deltaTime;
        if(currentTime <= 0)
        {
            currentTime = 0;
            isRuning = false;
            TimeUp();

        }
        UpdateTimeText();
    }
      void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60); // phút
        int seconds = Mathf.FloorToInt(currentTime % 60); // giây

        // Hiển thị 02:05 thay vì 2:5
        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    void TimeUp()
    {
        UIManager.Instance.ShowLose();
        Time.timeScale = 0;
    }
    void PauseTime()
    {
        isRuning = false;
    }
    void ResumeTime()
    {
        isRuning = true;
    }
    void ResetTime()
    {
        currentTime = startTime;
        isRuning = true;
        UpdateTimeText();
        
    }
}
