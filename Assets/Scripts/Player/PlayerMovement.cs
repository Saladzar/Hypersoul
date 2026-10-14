using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] float startingMoveSpeed = 5f;
    [SerializeField] int bounceScoreValue = 2;
    [SerializeField] private float stuckCheckDelay = 2f;

    private const float StuckMovementThreshold = 0.01f;
    private Vector2 previousPosition;
    private float stuckTimer;
    private float currentMoveSpeed;
    private Vector2 direction;
    private Rigidbody2D playerBody;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        direction = Vector2.right;
        currentMoveSpeed = startingMoveSpeed;
        previousPosition = playerBody.position;
    }

    void Update()
    {
        currentMoveSpeed = startingMoveSpeed * DifficultyTracker.Instance.GetCurrentDifficulty();

        Vector2 currentPosition = playerBody.position;
        if ((currentPosition - previousPosition).sqrMagnitude <= StuckMovementThreshold * StuckMovementThreshold)
        {
            stuckTimer += Time.deltaTime;
            if (stuckTimer >= stuckCheckDelay)
            {
                DestroyTouchingObstacles();
                stuckTimer = 0f;
            }
        }
        else
            stuckTimer = 0f;

        previousPosition = currentPosition;
    }

    void FixedUpdate()
    {
        playerBody.linearVelocity = direction * currentMoveSpeed;
    }

    private void DestroyTouchingObstacles()
    {
        List<Collider2D> touchingColliders = new List<Collider2D>();
        playerCollider.GetContacts(touchingColliders);

        foreach (Collider2D collider in touchingColliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
                Destroy(collider.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            PlayerAnimations.Instance.Impact();
            AudioPlayer.Instance.PlayAudioClip(AudioClipType.Bounce);

            Vector2 newDir = PlayerDirection.GetDirection();

            //Checks if direction points back into wall that player just bounced off of to have it bounce away instead if necessary.
            if (collision.contactCount > 0)
            {
                Vector2 contactNormal = collision.contacts[0].normal;
                if (Vector2.Dot(newDir, contactNormal) < 0f)
                    newDir = Vector2.Reflect(newDir, contactNormal);
            }

            direction = newDir;
            ScoreManager.Instance.AddBounceScore(bounceScoreValue);
        }
    }
}
