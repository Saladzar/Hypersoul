using UnityEngine;

public class ChaserController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Chaser Settings")]
    [SerializeField] private float startingMoveSpeed = 3f;
    [SerializeField] private float maxDistance = 50f;
    [SerializeField] private float difficultyMultiplier = 0.5f;

    private float currentMoveSpeed;

    void Start()
    {
        currentMoveSpeed = startingMoveSpeed;
    }

    void Update()
    {
        transform.position += currentMoveSpeed * Time.deltaTime * Vector3.right;

        if (Vector3.Distance(transform.position, player.position) > maxDistance)
            transform.position = new Vector3(player.position.x - maxDistance, transform.position.y, transform.position.z);

        currentMoveSpeed = startingMoveSpeed * (1f + DifficultyTracker.Instance.GetCurrentDifficulty()) * difficultyMultiplier;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            MenuController.Instance.OnPlayerLose();
    }
}
