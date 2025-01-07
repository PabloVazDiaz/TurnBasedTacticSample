using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    private const float MIN_FOLLOW_Y_OFFSET = 2F;
    private const float MAX_FOLLOW_Y_OFFSET = 12F;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float zoomAmount = 1f;
    [SerializeField] private float zoomSmoothing = 1f;

    void Update()
    {
        HandleTranslation();
        HandleRotation();
        HandleZoom();
    }

    private void HandleZoom()
    {
        CinemachineTransposer cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        Vector3 followOffset = cinemachineTransposer.m_FollowOffset;
        if (Input.mouseScrollDelta.y > 0)
        {
            followOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            followOffset.y += zoomAmount;
        }
        followOffset.y = Mathf.Clamp(followOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        followOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, followOffset, Time.deltaTime * zoomSmoothing);
        cinemachineTransposer.m_FollowOffset = followOffset;
    }

    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1;
        }
        
        transform.eulerAngles += rotationSpeed * Time.deltaTime * rotationVector;
    }

    private void HandleTranslation()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1;
        }

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveSpeed * Time.deltaTime * moveVector.normalized;
    }
}
