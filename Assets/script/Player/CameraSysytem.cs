using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSysytem : MonoBehaviour
{
    public Vector3 ShiftCam;
    public float SpeedMoveCam;

    Vector3 playerPosition, speedMoveCam, camPositionOld, camPositionNew, camPositionStart;
    Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        camPositionStart = transform.position;
        speedMoveCam = new Vector3(SpeedMoveCam, SpeedMoveCam, SpeedMoveCam);
    }

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            playerPosition = playerTransform.position;
            camPositionNew.x += (playerPosition.x - camPositionOld.x) * speedMoveCam.x * Time.deltaTime;
            camPositionNew.y += (playerPosition.y - camPositionOld.y) * speedMoveCam.y * Time.deltaTime;
            camPositionNew.z += (playerPosition.z - camPositionOld.z) * speedMoveCam.z * Time.deltaTime;
            transform.position = (camPositionStart + camPositionNew) + ShiftCam;
            camPositionOld = camPositionNew;
        }
    }
}
