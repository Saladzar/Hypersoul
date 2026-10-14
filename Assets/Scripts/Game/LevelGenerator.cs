using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject backgroundPrefab;

    [Header("Generation Settings")]
    [SerializeField] private float chunkWidth = 20f;
    [SerializeField] private float generateDistance = 40f;
    [SerializeField] private float despawnDistance = 60f;
    [SerializeField] private float waitTime = 1f; // Time to wait before generating the first chunk

    [Header("Obstacle Settings")]
    [SerializeField] private int minObstacleCount = 3;
    [SerializeField] private int maxObstacleCount = 8;
    [SerializeField] private float maxObstacleY = 8f;
    [SerializeField] private float minObstacleY = -8f;
    [SerializeField] private float minObstacleScale = 0.1f;
    [SerializeField] private float maxObstacleScale = 1.5f;

    private bool isGenerating = false;
    private float furthestGeneratedX = 0f;
    private float furthestPlayerX = 0f;
    private List<GameObject> generatedObjects = new List<GameObject>();

    void Start()
    {
        furthestPlayerX = player.position.x;
        furthestGeneratedX = player.position.x + chunkWidth;
        isGenerating = false;

        StartCoroutine(GenerateInitialChunk());
    }

    void Update()
    {
        furthestPlayerX = Mathf.Max(furthestPlayerX, player.position.x);

        if (furthestPlayerX + generateDistance > furthestGeneratedX && isGenerating)
            GenerateChunk();

        DespawnObjects();
    }

    private void GenerateChunk()
    {
        float chunkStartX = Mathf.Max(furthestGeneratedX, player.position.x + generateDistance);
        bool validChunk = false;

        for (int attempt = 0; attempt < ChunkValidator.Instance.maxAttempts; attempt++)
        {
            List<GameObject> candidates = new List<GameObject>();

            int obstacleCount = Random.Range(minObstacleCount, maxObstacleCount + 1);

            for (int i = 0; i < obstacleCount; i++)
            {
                float x = Random.Range(chunkStartX, chunkStartX + chunkWidth);
                float y = Random.Range(minObstacleY, maxObstacleY);
                float rotation = Random.Range(0f, 360f);
                float scale = Random.Range(minObstacleScale, maxObstacleScale);

                GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

                GameObject obstacle = Instantiate(prefab, new Vector2(x, y), Quaternion.Euler(0, 0, rotation));

                obstacle.transform.localScale = new Vector3(scale, scale, 1f);

                candidates.Add(obstacle);
            }

            Physics2D.SyncTransforms();

            if (ChunkValidator.Instance.HasValidPath(chunkStartX, chunkWidth, minObstacleY, maxObstacleY))
            {
                generatedObjects.AddRange(candidates);
                validChunk = true;
                break;
            }

            foreach (GameObject obstacle in candidates)
                Destroy(obstacle);

            Physics2D.SyncTransforms();
        }

        if (!validChunk)
            Debug.LogWarning($"Could not generate valid chunk at X={chunkStartX}.");

        furthestGeneratedX += chunkWidth;

        CameraRotation.Instance.OnChunkGenerated();
    }

    private void DespawnObjects()
    {
        for (int i = generatedObjects.Count - 1; i >= 0; i--)
        {
            GameObject gObject = generatedObjects[i];
            if (gObject == null)
            {
                generatedObjects.RemoveAt(i);
                continue;
            }

            if (gObject.transform.position.x < furthestPlayerX - despawnDistance)
            {
                generatedObjects.RemoveAt(i);
                Destroy(gObject);
            }
        }
    }

    IEnumerator GenerateInitialChunk()
    {
        yield return new WaitForSeconds(waitTime);
        isGenerating = true;
    }
}
