using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOVAdjust : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook freeLookCamera;
    [SerializeField] float minFOV;
    [SerializeField] float maxFOV;
    [SerializeField] float sensitivity;

    void Update()
    {
        float fov = freeLookCamera.m_Lens.FieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFOV, maxFOV);
        freeLookCamera.m_Lens.FieldOfView = fov;
    }
}
