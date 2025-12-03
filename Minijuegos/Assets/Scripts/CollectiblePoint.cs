using UnityEngine;
using UnityEngine.UI;

public class CollectiblePoint : MonoBehaviour
{
    public float collectRadius = 5f;

    [Header("UI")]
    
    public Button collectButton;

    private Transform player;
    private bool playerInside = false;
    private bool collected = false;

    private void Awake()
    {
        
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
        else
            Debug.LogError("No se encontró ningún objeto con tag 'Player'.");

        // Si no lo has asignado en el inspector, intenta buscarlo por nombre
        if (collectButton == null)
        {
            GameObject buttonObj = GameObject.Find("CollectButton");
            if (buttonObj != null)
                collectButton = buttonObj.GetComponent<Button>();
        }

        if (collectButton == null)
        {
            Debug.LogError("No se encontró el botón CollectButton. Asigna el Button en el inspector.");
            return;
        }

        // Ocultar botón al inicio
        collectButton.gameObject.SetActive(false);

        // Asegurar que el botón llama a Collect() (y solo a Collect)
        collectButton.onClick.RemoveAllListeners();
        collectButton.onClick.AddListener(Collect);
    }

    private void Update()
    {
        if (collected || player == null || collectButton == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        bool insideNow = dist <= collectRadius;

        if (insideNow && !playerInside)
        {
            playerInside = true;
            collectButton.gameObject.SetActive(true);
        }
        else if (!insideNow && playerInside)
        {
            playerInside = false;
            collectButton.gameObject.SetActive(false);
        }
    }

    public void Collect()
    {
        if (!playerInside || collected) return;

        collected = true;

        // Ocultar botón
        if (collectButton != null)
            collectButton.gameObject.SetActive(false);

        // Avisar al GameManager
        if (GPSGameManager.Instance != null)
            GPSGameManager.Instance.OnPointCollected();

        // Eliminar el punto
        Destroy(gameObject);
    }

    public bool IsCollected()
    {
        return collected;
    }
}
