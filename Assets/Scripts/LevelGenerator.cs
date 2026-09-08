using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject obstaclePrefab;
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
    [SerializeField] private float minObstacleScale = 0.5f;
    [SerializeField] private float maxObstacleScale = 2f;

    private bool isGenerating = false;
    private float furthestGeneratedX = 0f;
    private float furthestPlayerX = 0f;
    private List<GameObject> generatedObjects = new List<GameObject>();

    void Start()
    {
        furthestGeneratedX = 0f;
        furthestPlayerX = 0f;

        SpawnWalls(0f);
        
        for (int i = -1; i < 3; i++)
            SpawnBackground(i * chunkWidth);

        StartCoroutine(GenerateInitialChunk());
    }

    void Update()
    {
        furthestPlayerX = Mathf.Max(furthestPlayerX, player.position.x);

        if (furthestPlayerX + generateDistance > furthestGeneratedX)
            GenerateChunk();

        DespawnObjects();
    }

    private void GenerateChunk()
    {
        float chunkStartX = furthestGeneratedX;
        //float difficulty = Mathf.Clamp01(furthestPlayerX / 100f);

        int obstacleCount = Random.Range(minObstacleCount, maxObstacleCount + 1);

        if (isGenerating)
        {
            for (int i = 0; i < obstacleCount; i++)
            {
                float randomX = Random.Range(chunkStartX, chunkStartX + chunkWidth);
                float randomY = Random.Range(minObstacleY, maxObstacleY);
                float randomRotation = Random.Range(0f, 360f);
                float randomYScale = Random.Range(minObstacleScale, maxObstacleScale);
                float randomXScale = Random.Range(minObstacleScale, maxObstacleScale);
                Vector3 randomScale = new Vector3(randomXScale, randomYScale, 1f);

                SpawnObstacle(new Vector2(randomX, randomY), Quaternion.Euler(0, 0, randomRotation), randomScale);
            }
        }

        SpawnWalls(chunkStartX + chunkWidth / 2f);
        SpawnBackground(chunkStartX + chunkWidth / 2f);

        furthestGeneratedX += chunkWidth;
    }

    private void SpawnObstacle(Vector2 vector2, Quaternion rotation, Vector3 scale)
    {
        GameObject obstacle = Instantiate(obstaclePrefab, vector2, rotation);
        obstacle.transform.localScale = scale;
        generatedObjects.Add(obstacle);
    }

    private void SpawnWalls(float x)
    {
        GameObject wall = Instantiate(wallPrefab, new Vector2(x, 0), Quaternion.identity);
        generatedObjects.Add(wall);
    }

    private void SpawnBackground(float x)
    {
        return;
        // GameObject background = Instantiate(backgroundPrefab, new Vector2(x, 0), Quaternion.identity);
        // generatedObjects.Add(background);
    }

    private void DespawnObjects()
    {
        for (int i = generatedObjects.Count - 1; i >= 0; i--)
        {
            GameObject gObject = generatedObjects[i];
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
