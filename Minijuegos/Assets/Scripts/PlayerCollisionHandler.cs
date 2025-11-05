using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private GameManagerJuego1 gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManagerJuego1>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
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

