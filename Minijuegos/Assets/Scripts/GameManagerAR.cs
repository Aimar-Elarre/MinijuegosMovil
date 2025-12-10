using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    SearchingMarker,
    Playing,
    GameOver
}

public class GameManagerAR : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelSearching;
    public GameObject panelHUD;
    public GameObject panelGameOver;

    public Text scoreText;
    public Text failsText;
    public Text fruitNameText;
    public Text gameOverText;

    [Header("Puntuación")]
    public int score = 0;
    public int fails = 0;
    public int maxFails = 3;
    public int targetScore = 10;

    [HideInInspector] public GameState state = GameState.SearchingMarker;

    bool markerVisible = false;

    private void Start()
    {
        UpdateUI();
        SetState(GameState.SearchingMarker);
    }

    public void OnMarkerFound()
    {
        markerVisible = true;

        if (state == GameState.SearchingMarker)
            SetState(GameState.Playing);
    }

    public void OnMarkerLost()
    {
        markerVisible = false;
        // Lo usamos para pausar HUD si quieres:
        if (state == GameState.Playing)
        {
            panelHUD.SetActive(false);
            panelSearching.SetActive(true);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score < 0) score = 0;

        if (score >= targetScore)
        {
            EndGame(true);
        }

        UpdateUI();
    }

    public void AddFail()
    {
        fails++;
        if (fails >= maxFails)
        {
            EndGame(false);
        }
        UpdateUI();
    }

    void EndGame(bool win)
    {
        SetState(GameState.GameOver);
        gameOverText.text = win ? "¡Victoria!\nPuntos: " + score : "Has perdido...\nPuntos: " + score;
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Puntos: " + score;
        if (failsText != null) failsText.text = "Fallos: " + fails;
    }

    void SetState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.SearchingMarker:
                panelSearching.SetActive(true);
                panelHUD.SetActive(false);
                panelGameOver.SetActive(false);
                break;

            case GameState.Playing:
                panelSearching.SetActive(false);
                panelHUD.SetActive(true);
                panelGameOver.SetActive(false);
                break;

            case GameState.GameOver:
                panelSearching.SetActive(false);
                panelHUD.SetActive(false);
                panelGameOver.SetActive(true);
                break;
        }
    }

    public void RestartGame()
    {
        score = 0;
        fails = 0;
        UpdateUI();
        if (markerVisible)
        {
            SetState(GameState.Playing);
        }
        else
        {
            SetState(GameState.SearchingMarker);
        }
    }

    public void SetFruitName(string name)
    {
        if (fruitNameText != null)
            fruitNameText.text = "Fruta actual: " + name;
    }
}
