using UnityEngine;
using UnityEngine.Events;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private GameManagerJuego1 gm;
    [Header("Colision roca")]
    [SerializeField] public UnityEvent collisionrock;
    [Header("Colision gasolina")]
    [SerializeField] public UnityEvent collisiongsolinsa;

    private void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Roca"))
        {
            gm.GolpeContraRoca();
            Destroy(other.gameObject);
            collisionrock.Invoke();
        }
        else if (other.CompareTag("Gasolina"))
        {
            gm.RecogerGasolina();
            Destroy(other.gameObject);
            collisiongsolinsa.Invoke();
        }
    }
}

