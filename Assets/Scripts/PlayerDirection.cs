using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    public static Vector2 GetDirection()
    {
        float angle = ArrowController.angle;
        float angleInRadians = angle * Mathf.Deg2Rad;

        float x = Mathf.Cos(angleInRadians);
        float y = Mathf.Sin(angleInRadians);

        return new Vector2(x, y).normalized;
    }
}
