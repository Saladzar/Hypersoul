using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowController : MonoBehaviour
{
    public static float angle { get; private set; }

    void Update()
    {
        if (Time.timeScale == 0f)
            return;
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
