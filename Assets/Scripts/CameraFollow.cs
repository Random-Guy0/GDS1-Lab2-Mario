using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 playerPos = player.transform.position;
        if (mainCamera.WorldToViewportPoint(playerPos).x >= 0.5)
        {
            Vector3 cameraPos = mainCamera.transform.position;
            cameraPos.x = playerPos.x;
            mainCamera.transform.position = cameraPos;
        }
    }
}
