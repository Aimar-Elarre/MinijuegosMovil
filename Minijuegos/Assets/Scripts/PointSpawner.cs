using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject pointPrefab;
    public int pointsToSpawn = 3;
    public float radius = 150f;
    public float minDistanceBetweenPoints = 50f;

    private void Start()
    {
        SpawnPoints();
    }

    private void SpawnPoints()
    {
        if (player == null || pointPrefab == null)
        {
            Debug.LogWarning("Falta player o pointPrefab en el Spawner.");
            return;
        }

        Vector3 center = player.position;
        Vector3[] spawnedPositions = new Vector3[pointsToSpawn];
        int spawned = 0;
        int safety = 0;

        while (spawned < pointsToSpawn && safety < 1000)
        {
            safety++;

            Vector2 randomDir = Random.insideUnitCircle.normalized * Random.Range(minDistanceBetweenPoints, radius);
            Vector3 candidatePos = center + new Vector3(randomDir.x, 0f, randomDir.y);

            bool tooClose = false;
            for (int i = 0; i < spawned; i++)
            {
                if (Vector3.Distance(candidatePos, spawnedPositions[i]) < minDistanceBetweenPoints)
                {
                    tooClose = true;
                    break;
                }
            }

            if (tooClose) continue;

            GameObject newPoint = Instantiate(pointPrefab, candidatePos, Quaternion.identity);
            spawnedPositions[spawned] = candidatePos;
            spawned++;
        }

        if (GPSGameManager.Instance != null)
        {
            GPSGameManager.Instance.totalPointsToCollect = pointsToSpawn;
        }
    }
}
