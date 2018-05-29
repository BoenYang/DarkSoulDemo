using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;

    public Camera Camera;
   
    public float Height = 2f;

    public float Distance = 4.5f;

    public float FollowSpeed = 5;

    public float RotationSpeed = 30;

    public float Smooth = 0.1f;

    public float MaxTiltAngle = 10;

    public float MinTiltAngle = -10;

    public Transform LockTarget;

    public bool lockon;

    [HideInInspector]
    public float MouseX;

    [HideInInspector]
    public float MouseY;

    private Transform pivot;

    private Transform cameraHolder;

    private float lookAngle;

    private float tiltAngle;

    private float smoothX;

    private float smoothY;

    private float smoothXVelocity;

    private float smoothYVelocity;

    public void Start()
    {
        Target = gameObject.transform;
        pivot = Camera.transform.parent;
        cameraHolder = pivot.parent;
    }

    public void Tick(float dt)
    {
        FollowTarget(dt);
        HandleRotation(dt);
    }

    private void FollowTarget(float dt)
    {
        cameraHolder.transform.position = Vector3.Lerp(cameraHolder.position, Target.position, dt * FollowSpeed);
    }

    private void HandleRotation(float dt)
    {

        if (Smooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, MouseX, ref smoothXVelocity, Smooth);
            smoothY = Mathf.SmoothDamp(smoothY, MouseY, ref smoothYVelocity, Smooth);
        }
        else
        {
            smoothX = MouseX;
            smoothY = MouseY;
        }

        tiltAngle -= RotationSpeed * dt * smoothY;

        tiltAngle = Mathf.Clamp(tiltAngle, MinTiltAngle, MaxTiltAngle);
        pivot.localRotation = Quaternion.Euler(tiltAngle, 0,0);
 
        if (lockon && LockTarget != null)
        {
            Vector3 targetDir = LockTarget.position - cameraHolder.position ;
            targetDir.Normalize();

            if (targetDir == Vector3.zero)
            {
                targetDir = cameraHolder.forward;
            }

            Quaternion targetRot = Quaternion.LookRotation(targetDir);
            cameraHolder.rotation = Quaternion.Slerp(cameraHolder.rotation,targetRot, dt * 9);
            lookAngle = cameraHolder.eulerAngles.y;
            return;
        }

        lookAngle += RotationSpeed * dt * smoothX;
        cameraHolder.rotation = Quaternion.Euler(0, lookAngle, 0);
    }

    public Vector3 GetCameraForword()
    {
        return cameraHolder.forward;
    }

    public Vector3 GetCameraRight()
    {
        return cameraHolder.right;
    }
}
