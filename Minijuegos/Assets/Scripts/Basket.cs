using UnityEngine;

public class Basket : MonoBehaviour
{
    public bool esCestaFrutas = true; // si false = cesta verduras
    public FruitSpawner spawner;

    public GameObject fruit1;
    public GameObject fruit2;

    public bool cesta1;

    private void OnTriggerEnter(Collider other)
    {
        var throwable = other.GetComponent<ThrowableObject>();
        if (throwable == null) return;

        bool esFruta = spawner.EsFrutaActual();

        bool acierto = (esFruta && esCestaFrutas) || (!esFruta && !esCestaFrutas);

        if (acierto)
        {
            spawner.NotificarAcierto();
            // aquí añadimos efectos y sonido de acierto
        }
        else
        {
            spawner.NotificarFallo();
            // aquí añadimos efectos y sonido de error
        }

        Destroy(other.gameObject);
    }
}
