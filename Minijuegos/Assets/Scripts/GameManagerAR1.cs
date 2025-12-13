using TMPro;
using UnityEngine;

public class GameManagerAR1 : MonoBehaviour
{
    public GameObject fruta;
    public int numeoDeFrutas = 4;
    public int multiplicadorPuntos = 1;
    [Header("UI")]
    public GameObject uiganar;
    public GameObject uiperder;
    public TextMeshProUGUI puntuacionUI;


    private int numeoFrutaActual = 0;
    private float puntuacion = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numeoFrutaActual == numeoDeFrutas)
        {
            uiganar.SetActive(true);
            puntuacionUI.text = "Puntos = " + puntuacion;
        }
    }
    public void Anotacion(GameObject cesta)
    {
        puntuacion = multiplicadorPuntos * Vector3.Distance(fruta.transform.position, cesta.transform.position);
        numeoFrutaActual++;
    }
    public void perder()
    {
        uiperder.SetActive(true);
    }
}
