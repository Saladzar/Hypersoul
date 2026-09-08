using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    private Vector2 direction;
    private Rigidbody2D playerBody;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        direction = PlayerDirection.GetDirection();
    }

    void FixedUpdate()
    {
        //playerBody.MovePosition(playerBody.position + direction * moveSpeed * Time.fixedDeltaTime);
        playerBody.linearVelocity = direction * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 newDir = PlayerDirection.GetDirection();

            //Checks if direction points back into wall that player just bounced off of.
            if (collision.contactCount > 0)
            {
                Vector2 contactNormal = collision.contacts[0].normal;
                if (Vector2.Dot(newDir, contactNormal) < 0f)
                {
                    //If it does, reverse the direction to point away from the wall.
                    newDir = Vector2.Reflect(newDir, contactNormal);
                }
            }

            direction = newDir;
        }
    }

    private float GetAngleOfMovement()
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
    }
}
