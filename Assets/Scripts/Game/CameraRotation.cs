using System;
using System.Collections;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public static CameraRotation Instance { get; private set; }
    
    [SerializeField] private float startingRotationSpeed = 10f;
    [SerializeField] private float startingRotationPause = 10f;
    [SerializeField] private float startingRotationProbability = 0.1f;
    [SerializeField] private float delayBeforeFirstRotation = 20f;

    private float currentRotationSpeed;
    private float currentRotationPause;
    private float currentRotationProbability;
    private float currentRotation = 0f;
    private float targetRotation = 0f;
    private bool isRotating = false;
    private bool isPaused = true;

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
        currentRotationSpeed = startingRotationSpeed;
        currentRotationPause = startingRotationPause;
        currentRotationProbability = startingRotationProbability;
        currentRotation = transform.rotation.eulerAngles.z;
        targetRotation = currentRotation;

        isPaused = true;

        StartCoroutine(InitialRotationDelay());
    }

    private IEnumerator InitialRotationDelay()
    {
        yield return new WaitForSeconds(delayBeforeFirstRotation);
        isPaused = false;
    }

    void Update()
    {
        if (currentRotation != targetRotation)
        {
            isRotating = true;
            currentRotation = Mathf.MoveTowards(currentRotation, targetRotation, currentRotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
        }
        else
        {
            if (isRotating)
            {
                isRotating = false;
                StartCoroutine(PauseRotation());
            }
        }

        currentRotationSpeed = startingRotationSpeed * DifficultyTracker.Instance.GetCurrentDifficulty();
        currentRotationPause = startingRotationPause / DifficultyTracker.Instance.GetCurrentDifficulty();
        currentRotationProbability = startingRotationProbability * DifficultyTracker.Instance.GetCurrentDifficulty();
    }

    public void OnChunkGenerated()
    {
        if (UnityEngine.Random.value < currentRotationProbability && !isRotating && !isPaused)
            RotateCamera();
    }

    public void RotateCamera()
    {
        float angle = UnityEngine.Random.Range(-90f, 90f);
        targetRotation = currentRotation + angle;
    }

    private IEnumerator PauseRotation()
    {
        isPaused = true;
        yield return new WaitForSeconds(currentRotationPause);
        isPaused = false;
    }
}
