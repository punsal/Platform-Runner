using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform runner;
    [Range(0, 1)]
    public float smoothSpeed = 0.125f;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - runner.position;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = runner.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            newPosition,
            smoothSpeed
            );
        transform.position = smoothedPosition;

        transform.LookAt(runner);
    }
}
