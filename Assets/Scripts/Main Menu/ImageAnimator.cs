using UnityEngine;

public class ImageAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform characterImage;
    [SerializeField] private RectTransform asteroidImage;
    [SerializeField] private float charRotationSpeed = 2f;
    [SerializeField] private float charRotationOffset = 15f;
    [SerializeField] private float astRotationSpeed = 5f;

    private float startAngle = 0f;

    void Start()
    {
        characterImage.localEulerAngles = new Vector3(0, 0, startAngle);
        asteroidImage.localEulerAngles = new Vector3(0, 0, startAngle);
    }

    void Update()
    {
        float angleOffset = Mathf.Sin(Time.unscaledTime * charRotationSpeed) * charRotationOffset;
        characterImage.localEulerAngles = new Vector3(0, 0, startAngle + angleOffset);

        asteroidImage.localEulerAngles = new Vector3(0, 0, startAngle + Time.unscaledTime * astRotationSpeed);
    }
}
