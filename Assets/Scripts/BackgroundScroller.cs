using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float scrollSpeed = 0.1f;

    private Material backgroundMaterial;
    private Vector2 offset;

    void Start()
    {
        backgroundMaterial = GetComponent<MeshRenderer>().material;
        offset = backgroundMaterial.mainTextureOffset;
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        offset.x = player.position.x * scrollSpeed;
        backgroundMaterial.mainTextureOffset = offset;
    }
}
