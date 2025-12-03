using UnityEngine;

public class HintArrow : MonoBehaviour
{
    public Transform player;
    public float showDuration = 5f;

    private CollectiblePoint[] allPoints;
    private float timer = 0f;
    private bool activeArrow = false;

    private void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        allPoints = FindObjectsOfType<CollectiblePoint>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!activeArrow || player == null) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            gameObject.SetActive(false);
            activeArrow = false;
            return;
        }

        CollectiblePoint closest = null;
        float closestDist = Mathf.Infinity;

        foreach (var p in allPoints)
        {
            if (p == null || p.IsCollected()) continue;

            float d = Vector3.Distance(player.position, p.transform.position);
            if (d < closestDist)
            {
                closestDist = d;
                closest = p;
            }
        }

        if (closest != null)
        {
            Vector3 dir = closest.transform.position - player.position;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.01f)
            {
                transform.position = player.position + Vector3.up * 2f;
                transform.forward = dir.normalized;
            }
        }
    }

   
    public void ShowHint()
    {
        if (allPoints == null || allPoints.Length == 0)
        {
            allPoints = FindObjectsOfType<CollectiblePoint>();
        }

        timer = showDuration;
        activeArrow = true;
        gameObject.SetActive(true);
    }
}
