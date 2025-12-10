using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrowableObject : MonoBehaviour
{
    public FruitSpawner spawner;

    private Rigidbody rb;
    private Vector2 startPos;
    private bool dragging = false;

    public float forceMultiplier = 0.02f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        if (Camera.main == null) return;
        startPos = Input.mousePosition;
        dragging = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void OnMouseUp()
    {
        if (!dragging) return;
        dragging = false;

        Vector2 endPos = Input.mousePosition;
        Vector2 delta = endPos - startPos;

        Vector3 force = new Vector3(delta.x, delta.y, delta.magnitude) * forceMultiplier;
        rb.AddForce(force, ForceMode.Impulse);
    }
}
