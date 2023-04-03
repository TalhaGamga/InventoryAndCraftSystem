using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    private const float SMOOTH_TIME = 0.1f;

    [SerializeField] private PlayerInput playerInput;

    [SerializeField] float minRotationX = -60f;
    [SerializeField] float maxRotationX = 50f;

    [SerializeField] private bool smoothDamp = false;

    private Vector3 rotation;
    private Vector3 currentVelocity;

    private float pitch;
    private float yaw;

    private void Start()
    {
        rotation = transform.eulerAngles;
    }

    private void LateUpdate()
    {
        if (playerInput == null) return;
        yaw += playerInput.horizontalMouseInput;
        pitch -= playerInput.verticalMouseInput;

        if (smoothDamp)
        {
            rotation = Vector3.SmoothDamp(rotation, new Vector3(pitch, yaw), ref currentVelocity, SMOOTH_TIME);

        }
        else
        {
            rotation = new Vector3(pitch, yaw, rotation.z);
        }
        rotation.x = ClampAngle(rotation.x, minRotationX, maxRotationX);
        transform.transform.rotation = Quaternion.Euler(rotation);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
        {
            angle += 360F;
        }
        if (angle > 360F)
        {
            angle -= 360F;
        }
        return Mathf.Clamp(angle, min, max);
    }
}