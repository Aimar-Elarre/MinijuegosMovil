using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private Transform bg1;
    [SerializeField] private Transform bg2;

    private float imageHeight;

    private void Start()
    {
        // Calculamos la altura del objeto (según el Renderer)
        Renderer rend = bg1.GetComponent<Renderer>();
        imageHeight = rend.bounds.size.y;
    }

    private void Update()
    {
        // Mover ambos hacia abajo
        bg1.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        bg2.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // Reposicionar cuando uno sale por abajo
        if (bg1.position.y < -imageHeight)
        {
            bg1.position = new Vector3(bg1.position.x, bg2.position.y + imageHeight, bg1.position.z);
        }

        if (bg2.position.y < -imageHeight)
        {
            bg2.position = new Vector3(bg2.position.x, bg1.position.y + imageHeight, bg2.position.z);
        }
    }
}
