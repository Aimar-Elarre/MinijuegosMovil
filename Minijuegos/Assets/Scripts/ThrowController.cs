using UnityEngine;

public class ThrowController : MonoBehaviour
{
    private Vector2 start;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        start = Input.mousePosition;
    }

    void OnMouseUp()
    {
        Vector2 end = Input.mousePosition;
        Vector2 dir = end - start;

        Vector3 fuerza = new Vector3(dir.x, dir.y, dir.magnitude) * 0.02f;

        rb.AddForce(fuerza, ForceMode.Impulse);
    }
}
