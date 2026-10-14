using UnityEngine;

public class TextThrobber : MonoBehaviour
{
    public static TextThrobber Instance { get; private set; }

    [SerializeField] private Transform textTransform;

    [SerializeField] private float throbSpeed = 2f;
    [SerializeField] private float throbAmount = 0.2f;

    private Vector3 originalScale;
    private bool isThrobbing = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        originalScale = textTransform.localScale;
    }

    void Update()
    {
        if (!isThrobbing) return;

        float scale = 1 + Mathf.Sin(Time.unscaledTime * throbSpeed) * throbAmount;
        textTransform.localScale = originalScale * scale;
    }

    public void StartThrobbing()
    {
        isThrobbing = true;
    }

    public void StopThrobbing()
    {
        isThrobbing = false;
    }
}
