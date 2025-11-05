using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private GameManagerJuego1 gm;

    private void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Roca"))
        {
            gm.GolpeContraRoca();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Gasolina"))
        {
            gm.RecogerGasolina();
            Destroy(other.gameObject);
        }
    }
}

