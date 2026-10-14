using UnityEngine;

public class DifficultyTracker : MonoBehaviour
{
    public static DifficultyTracker Instance { get; private set; }

    [SerializeField] private float difficultyIncreaseRate = 0.001f;
    [SerializeField] private float startingDifficulty = 1.0f;
    [SerializeField] private float currentDifficulty;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        currentDifficulty = startingDifficulty;
    }

    void Update()
    {
        currentDifficulty += difficultyIncreaseRate * Time.deltaTime;
    }

    public float GetCurrentDifficulty()
    {
        return currentDifficulty;
    }
}
