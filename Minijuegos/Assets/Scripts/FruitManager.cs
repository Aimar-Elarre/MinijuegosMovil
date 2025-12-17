using UnityEngine;

public class FruitManager : MonoBehaviour
{
    public GameObject[] frutas;        
    public Transform spawnPoint;
    public Basket basket;
    public int puntuacion = 0;

    private GameObject frutaActual;

    void SpawnFruta()
    {
        int r = Random.Range(0, frutas.Length);
        frutaActual = Instantiate(frutas[r], spawnPoint.position, Quaternion.identity);
    }

    public void Acierto()
    {
        puntuacion++;
        Destroy(frutaActual);
        SpawnFruta();
    }

    public void Error()
    {
        puntuacion--;
        Destroy(frutaActual);
        SpawnFruta();
    }
}
