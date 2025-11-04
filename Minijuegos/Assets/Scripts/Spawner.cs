using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject rocaPrefab;
    [SerializeField] private GameObject gasolinaPrefab;

    [Header("Frecuencia de spawns (segundos)")]
    [SerializeField] private Vector2 rocaInterval = new Vector2(0.9f, 1.4f);
    [SerializeField] private Vector2 gasInterval = new Vector2(2.5f, 3.5f);

    [Header("Velocidad")]
    [SerializeField] private float baseFallSpeed = 4f;
    [SerializeField] private bool randomRockScale = true;
    [SerializeField] private Vector2 rocaScaleRange = new Vector2(0.7f, 1.5f);

    [Header("Posición de spawn")]
    [SerializeField] private float spawnYOffset = 0.8f;
    [SerializeField] private float lateralPadding = 0.06f;
    [SerializeField] private Transform player;
    [SerializeField] private float zOffsetRelativeToPlayer = 1f;

    [Header("Z fijo (opcional)")]
    [SerializeField] private bool useFixedZ = true;   // ✅ Activa para usar Z absoluto
    [SerializeField] private float fixedZ = 4.6f;     // Valor Z fijo en tu escena

    private Camera cam;
    private float rocaT, gasT, nextRoca, nextGas;
    private GameManagerJuego1 gm;

    private void Start()
    {
        cam = Camera.main;
        gm = FindObjectOfType<GameManagerJuego1>();

        nextRoca = Random.Range(rocaInterval.x, rocaInterval.y);
        nextGas = Random.Range(gasInterval.x, gasInterval.y);
    }

    private void Update()
    {
        float mult = gm ? gm.Dificultad : 1f;

        rocaT += Time.deltaTime;
        gasT += Time.deltaTime;

        if (rocaT >= nextRoca / mult)
        {
            rocaT = 0f;
            nextRoca = Random.Range(rocaInterval.x, rocaInterval.y);
            Spawn(rocaPrefab, true);
        }

        if (gasT >= nextGas / mult)
        {
            gasT = 0f;
            nextGas = Random.Range(gasInterval.x, gasInterval.y);
            Spawn(gasolinaPrefab, false);
        }
    }

    private void Spawn(GameObject prefab, bool isRock)
    {
        if (cam == null) return;

        float xView = Random.Range(lateralPadding, 1f - lateralPadding);
        Vector3 topWorld;

        // --- Calculamos el Z final ---
        float zFinal = useFixedZ
            ? fixedZ
            : (player ? player.position.z + zOffsetRelativeToPlayer : 0f);

        // --- Si la cámara es ortográfica ---
        if (cam.orthographic)
        {
            float halfH = cam.orthographicSize;
            float halfW = halfH * cam.aspect;

            float x = Mathf.Lerp(cam.transform.position.x - halfW, cam.transform.position.x + halfW, xView);
            float y = cam.transform.position.y + halfH + spawnYOffset;

            topWorld = new Vector3(x, y, zFinal);
        }
        else
        {
            // Para cámara en perspectiva
            float distZ = (player ? Vector3.Dot(player.position - cam.transform.position, cam.transform.forward) : 5f);
            topWorld = cam.ViewportToWorldPoint(new Vector3(xView, 1f + spawnYOffset, distZ));
            topWorld.z = zFinal;
        }

        // --- Instanciamos el prefab ---
        var go = Instantiate(prefab, topWorld, Quaternion.identity);

        // Escala aleatoria para rocas
        if (isRock && randomRockScale)
        {
            float s = Random.Range(rocaScaleRange.x, rocaScaleRange.y);
            go.transform.localScale = new Vector3(s, s, s);
        }

        // Velocidad según dificultad
        if (go.TryGetComponent<FallingObject>(out var fo))
        {
            float mult = gm ? gm.Dificultad : 1f;
            fo.SetSpeed(baseFallSpeed * mult);
        }
    }
}
