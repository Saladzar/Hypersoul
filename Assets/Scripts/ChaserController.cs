using UnityEngine;

public class ChaserController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Chaser Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float maxDistance = 50f;

    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.right;

        if (Vector3.Distance(transform.position, player.position) > maxDistance)
        {
            transform.position = new Vector3(player.position.x - maxDistance, transform.position.y, transform.position.z);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            MenuController.Instance.OnPlayerLose();
    }
}
