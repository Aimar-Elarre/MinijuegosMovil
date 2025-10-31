using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 5f;  //velocidad lateral
    [SerializeField] private float limiteX = 3f; //límite horizontal para no salirte

    private Camera cam;
    private Vector3 touchStartPos;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        MoverConTeclado();   //para probar en PC
        MoverConTouch();     //para móvil
        LimitarPosicion();
    }

    private void MoverConTeclado()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A/D, flechas, izquierda/derecha
        transform.Translate(Vector2.right * h * speed * Time.deltaTime);
    }

    private void MoverConTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            // si el dedo se está moviendo empujamos al jugador en X hacia donde está el dedo
            if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
            {
                Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, 10f));
                // nos movemos hacia esa x suavemente
                float step = speed * Time.deltaTime;
                Vector3 pos = transform.position;
                pos.x = Mathf.MoveTowards(pos.x, worldPos.x, step);
                transform.position = pos;
            }
        }
    }

    private void LimitarPosicion()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -limiteX, limiteX);
        transform.position = pos;
    }
}
