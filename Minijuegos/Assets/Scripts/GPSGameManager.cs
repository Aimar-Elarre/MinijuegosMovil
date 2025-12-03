using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GPSGameManager : MonoBehaviour
{
    public static GPSGameManager Instance;

    [Header("Configuracion")]
    public float timeLimitSeconds = 300f;
    [HideInInspector] public int totalPointsToCollect = 3;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI finalScoreText;

    private float timeRemaining;
    private int collectedPoints = 0;
    private bool gameRunning = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        collectedPoints = 0;
        timeRemaining = timeLimitSeconds;
        gameRunning = true;

        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);
    }

    private void Update()
    {
        if (!gameRunning) return;

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0f, timeRemaining);

        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }

        if (timeRemaining <= 0f)
        {
            LoseGame();
        }
    }

    public void OnPointCollected()
    {
        collectedPoints++;
        if (collectedPoints >= totalPointsToCollect)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        if (!gameRunning) return;

        gameRunning = false;

        
        int score = Mathf.RoundToInt(1000f * (timeRemaining / timeLimitSeconds));

        if (winPanel) winPanel.SetActive(true);
        if (finalScoreText) finalScoreText.text = "Puntuación: " + score;
    }

    public void LoseGame()
    {
        if (!gameRunning) return;

        gameRunning = false;
        if (losePanel) losePanel.SetActive(true);
    }
}
