using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // si sale muy abajo de la pantalla, destruir
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
