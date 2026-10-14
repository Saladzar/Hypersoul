using UnityEngine;
using UnityEngine.Tilemaps;

public class BoundaryScroller : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField, Range(0f, 1f)] private float scrollFactor = 0.1f;
    [SerializeField] private float scrollSpeed = 0.2f;

    private Material boundaryMaterial;
    private float playerStartX;

    void Start()
    {
        TilemapRenderer tilemapRenderer = GetComponent<TilemapRenderer>();
        boundaryMaterial = tilemapRenderer.material;

        playerStartX = player.position.x;
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);

        float distanceMoved = player.position.x - playerStartX;
        float offset = distanceMoved * scrollFactor;
        boundaryMaterial.mainTextureOffset = new Vector2(offset, 0f);
    }
}
