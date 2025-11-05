using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerJuego1 : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Slider gasolinaBar;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private Button volverMenuBtn;
    [SerializeField] private TMP_Text vidasText;

    [Header("Gasolina")]
    [SerializeField] private float gasolinaMax = 100f;
    [SerializeField] private float consumoPorSegundo = 5f;
    [SerializeField] private float recargaBidon = 30f;

    [Header("Vidas")]
    [SerializeField] private int vidas = 3;

    [Header("Puntuación")]
    [SerializeField] private float puntosPorSegundo = 10f;

    [Header("Dificultad")]
    [SerializeField] private float crecimientoPorSegundo = 0.06f;
    [SerializeField] private float dificultadMax = 3f;

    private float gasolinaActual;
    private float score;
    private bool gameOver = false;
    private float tiempo = 0f;

    public float Dificultad => Mathf.Min(1f + tiempo * crecimientoPorSegundo, dificultadMax);

    private void Start()
    {
        gasolinaActual = gasolinaMax;
        score = 0f;
        gameOverPanel.SetActive(false);

        
        if (volverMenuBtn != null)
            volverMenuBtn.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        else
            Debug.LogWarning("⚠️ No se ha asignado el botón Volver al Menú.");
    }

    private void Update()
    {
        if (gameOver) return;

        tiempo += Time.deltaTime;

        
        gasolinaActual = Mathf.Max(0f, gasolinaActual - consumoPorSegundo * Dificultad * Time.deltaTime);

        
        score += puntosPorSegundo * Time.deltaTime * Dificultad;

        
        if (gasolinaBar != null)
            gasolinaBar.value = gasolinaActual / gasolinaMax;

        if (scoreText != null)
            scoreText.text = "Distancia: " + Mathf.FloorToInt(score);

        if (vidasText != null)
            vidasText.text = "Vidas: " + Mathf.FloorToInt(vidas);


        if (gasolinaActual <= 0f)
            Perder("Te has quedado sin gasolina");
    }

    
    public void GolpeContraRoca()
    {
        if (gameOver) return;
        if (vidas == 1)
        {
            Perder("Has chocado con una roca");
        }
        else
        {
            vidas -= 1;
        }
    }

    
    public void RecogerGasolina()
    {
        if (gameOver) return;
        gasolinaActual = Mathf.Min(gasolinaMax, gasolinaActual + recargaBidon);
    }

    
    private void Perder(string motivo)
    {
        gameOver = true;

        if (player != null)
            player.enabled = false;

        
        var spawner = FindObjectOfType<Spawner>();
        if (spawner != null)
            spawner.enabled = false;

        
        gameOverPanel.SetActive(true);
        gameOverText.text = $"HAS PERDIDO\n{motivo}\nDistancia: {Mathf.FloorToInt(score)}";
    }
}
