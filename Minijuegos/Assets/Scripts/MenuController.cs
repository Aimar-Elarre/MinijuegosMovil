using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Referencias a los botones")]
    [SerializeField] private Button botonGame1;
    [SerializeField] private Button botonGame2;
    [SerializeField] private Button botonGame3;
    [SerializeField] private Button botonSocial;

    
    [Header("Nombres de escenas")]
    [SerializeField] private string escenaGame1 = "Game1";
    [SerializeField] private string escenaGame2 = "Game2";
    [SerializeField] private string escenaGame3 = "Game3";
    [SerializeField] private string escenaSocial = "Social";

    private void Start()
    {
        
        botonGame1.onClick.AddListener(() => CargarEscena(escenaGame1));
        botonGame2.onClick.AddListener(() => CargarEscena(escenaGame2));
        botonGame3.onClick.AddListener(() => CargarEscena(escenaGame3));
        botonSocial.onClick.AddListener(() => CargarEscena(escenaSocial));
    }

    private void CargarEscena(string nombreEscena)
    {
        
        SceneManager.LoadScene(nombreEscena);
    }
}
