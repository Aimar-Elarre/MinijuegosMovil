using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private float speed = 4f;  

    
    public void SetSpeed(float s)
    {
        speed = s;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (Camera.main != null)
        {
            var bottom = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, -0.2f, Mathf.Abs(Camera.main.transform.position.z) + 1f));
            if (transform.position.y < bottom.y)
                Destroy(gameObject);
        }
    }
}
