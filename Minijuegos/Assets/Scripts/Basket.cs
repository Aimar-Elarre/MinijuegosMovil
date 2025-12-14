using UnityEngine;

public class Basket : MonoBehaviour
{
    [Header("Tipo de cesta")]
    public bool esCestaFrutas = true; 

    [Header("Referencias")]
    public FruitSpawner spawner;

   
    public Transform spawnPoint;

    [Header("Puntos por distancia")]
    public float maxDistance = 2f;   
    public int maxExtraPoints = 5;   
    public bool ignorarAltura = true; 

    private void OnTriggerEnter(Collider other)
    {
        var throwable = other.GetComponent<ThrowableObject>();
        if (throwable == null) return;

        if (spawner == null)
        {
            Debug.LogWarning("[Basket] Spawner no asignado.");
            Destroy(other.gameObject);
            return;
        }

        bool esFruta = spawner.EsFrutaActual();
        bool acierto = (esFruta && esCestaFrutas) || (!esFruta && !esCestaFrutas);

        if (acierto)
        {
            int puntos = 1;

            
            if (spawnPoint != null)
            {
                Vector3 a = spawnPoint.position;
                Vector3 b = transform.position;

                if (ignorarAltura)
                {
                    a.y = 0;
                    b.y = 0;
                }

                float d = Vector3.Distance(a, b);

                float t = Mathf.Clamp01(d / maxDistance); 
                int extra = Mathf.RoundToInt(t * maxExtraPoints);

                puntos += extra;
            }

            
            AudioManagerFruit.Instance?.PlaySuccess();

            spawner.NotificarAcierto(puntos);
        }
        else
        {
            AudioManagerFruit.Instance?.PlayFail();
            spawner.NotificarFallo();
        }

        Destroy(other.gameObject);
    }
}
