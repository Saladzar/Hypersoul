using UnityEngine;
using Unity.Cinemachine;

[ExecuteAlways]
[AddComponentMenu("Cinemachine/Extensions/Cinemachine Lock Y Position")]
public class CinemachineLockY : CinemachineExtension
{
    public float TargetYPosition = 0f;
    public float SmoothSpeed = 5f;

    private float currentY;

    protected override void OnEnable()
    {
        base.OnEnable();
        currentY = TargetYPosition; 
    }

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            if (Application.isPlaying && deltaTime >= 0)
                currentY = Mathf.Lerp(currentY, TargetYPosition, deltaTime * SmoothSpeed);
            else
                currentY = TargetYPosition;

            Vector3 pos = state.RawPosition;
            pos.y = currentY;
            state.RawPosition = pos;
        }
    }
}
