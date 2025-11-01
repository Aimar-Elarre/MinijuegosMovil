/*using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private GameManagerJuego1 gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManagerJuego1>();
    }

    private void OnTriggerEnter2D(Collider2D other)
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
*/
