using UnityEngine;

[System.Serializable]
public class FruitData
{
    public string nombre;
    public GameObject prefab;
    public bool esFruta; 
}

public class FruitSpawner : MonoBehaviour
{
    public FruitData[] frutas;
    public Transform spawnPoint;
    public GameManagerAR gameManager;

    private GameObject frutaActual;
    private FruitData frutaActualData;

    void Start()
    {
        SpawnNuevaFruta();
    }

    public void SpawnNuevaFruta()
    {
        if (frutaActual != null)
            Destroy(frutaActual);

        int index = Random.Range(0, frutas.Length);
        frutaActualData = frutas[index];

        frutaActual = Instantiate(frutaActualData.prefab, spawnPoint.position, Quaternion.identity, transform);

        // Añadimos el script de lanzar
        var throwable = frutaActual.GetComponent<ThrowableObject>();
        if (throwable == null)
            throwable = frutaActual.AddComponent<ThrowableObject>();

        throwable.spawner = this;

        if (gameManager != null)
            gameManager.SetFruitName(frutaActualData.nombre);
    }

    public void NotificarAcierto()
    {
        if (gameManager != null)
            gameManager.AddScore(1);

        SpawnNuevaFruta();
    }

    public void NotificarFallo()
    {
        if (gameManager != null)
            gameManager.AddFail();

        SpawnNuevaFruta();
    }

    public bool EsFrutaActual()
    {
        return frutaActualData != null && frutaActualData.esFruta;
    }
}
