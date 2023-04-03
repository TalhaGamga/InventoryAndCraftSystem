using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform target;
    [SerializeField] float cameraDistance = -2.4f;

    private void LateUpdate()
    {
        playerCamera.transform.localPosition = Vector3.forward * cameraDistance;
        playerCamera.transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.position = target.position;
    }
}
