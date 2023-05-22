using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform[] targets;             // Array of targets to follow (player characters)
    public float distance = 5f;             // Distance from the targets
    public float height = 2f;               // Height above the targets
    public float smoothSpeed = 10f;         // Smoothness of camera movement

    private Vector3 offset;                 // Offset from the targets

    private void Start()
    {
        // Calculate the initial offset from the targets
        offset = new Vector3(0f, height, -distance);
    }

    private void LateUpdate()
    {
        // Calculate the center point between the targets
        Vector3 centerPoint = GetCenterPoint();

        // Calculate the desired position for the camera
        Vector3 desiredPosition = centerPoint + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Look at the center point
        transform.LookAt(centerPoint);

        // Make the players always face each other
        for (int i = 0; i < targets.Length; i++)
        {
            Vector3 lookAtPoint = centerPoint;
            lookAtPoint.y = targets[i].position.y; // Maintain the same height as the player
            targets[i].LookAt(lookAtPoint);
        }
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Length == 0)
        {
            return Vector3.zero;
        }

        // Calculate the center point between the targets
        Vector3 centerPoint = targets[0].position;
        for (int i = 1; i < targets.Length; i++)
        {
            centerPoint += targets[i].position;
        }
        centerPoint /= targets.Length;

        return centerPoint;
    }
}

