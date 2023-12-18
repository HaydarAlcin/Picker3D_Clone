using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[SaveDuringPlay]
[AddComponentMenu("")]
public class LockCinemachineAxis : CinemachineExtension
{
    /// <summary>
    /// An attempt was made to limit the movement of the camera in the x-axis with the Position Constraint component,
    /// but due to the shaking movement, it was decided to add Execute.
    /// </summary>

    public float XClampValue = 0;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.x = XClampValue;
            state.RawPosition = pos;
        }

    }
}
