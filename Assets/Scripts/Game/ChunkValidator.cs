using System.Collections.Generic;
using UnityEngine;

public class ChunkValidator : MonoBehaviour
{
    public static ChunkValidator Instance { get; private set; }

    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private float playerRadius = 0.25f;
    [SerializeField] private int pathSamples = 15;
    [SerializeField] private int directionsPerBounce = 13;
    [SerializeField] private int maxBounces = 12;
    [SerializeField] private int maxTestStates = 1000;
    [SerializeField] private int maxAttempts = 20;

    private struct PathState
    {
        public Vector2 position;
        public Vector2 direction;
        public int bounces;

        public PathState(Vector2 pos, Vector2 dir, int b)
        {
            position = pos;
            direction = dir.normalized;
            bounces = b;
        }
    }

    public bool HasValidPath(float chunkStartX, float chunkWidth, float minObstacleY, float maxObstacleY)
    {
        float chunkEndX = chunkStartX + chunkWidth;

        Queue<PathState> paths = new Queue<PathState>();

        for (int i = 0; i < pathSamples; i++)
        {
            float t = pathSamples <= 1 ? 0.5f : (float) i / (pathSamples - 1);

            float y = Mathf.Lerp(minObstacleY + playerRadius, maxObstacleY - playerRadius, t);

            for (int j = -4; j <= 4; j++)
            {
                float angle = j * 15f;
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;

                paths.Enqueue(new PathState(new Vector2(chunkStartX + 0.01f, y), direction, 0));
            }
        }

        int statesTested = 0;

        while (paths.Count > 0 && statesTested < maxTestStates)
        {
            PathState state = paths.Dequeue();
            statesTested++;

            Vector2 pos = state.position;
            Vector2 dir = state.direction;

            if (pos.y < minObstacleY + playerRadius || pos.y > maxObstacleY - playerRadius)
                continue;

            float exitDistance = dir.x > 0.001f ? (chunkEndX - pos.x) / dir.x : float.PositiveInfinity;

            float castDistance = Mathf.Min(exitDistance, chunkWidth * 2f);

            RaycastHit2D hit = Physics2D.CircleCast(pos, playerRadius, dir, castDistance, obstacleLayers);

            if (!hit.collider)
            {
                if (dir.x > 0.001f && exitDistance > 0f && exitDistance <= chunkWidth * 2f)
                    return true;

                continue;
            }

            if (state.bounces >= maxBounces)
                continue;

            Vector2 normal = hit.normal;

            for (int i = 0; i < directionsPerBounce; i++)
            {
                float angle = 360f * i / directionsPerBounce;

                Vector2 newDir = Quaternion.Euler(0, 0, angle) * Vector2.right;

                if (Vector2.Dot(newDir, normal) < 0f)
                    newDir = Vector2.Reflect(newDir, normal);

                if (newDir.x <= 0.05f)
                    continue;

                Vector2 newPos = hit.centroid + normal * 0.02f;

                paths.Enqueue(new PathState(newPos, newDir, state.bounces + 1));
            }
        }

        return false;
    }
}
