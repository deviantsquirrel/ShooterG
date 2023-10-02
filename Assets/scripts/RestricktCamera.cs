using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestricktCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform boundaryTopRight; 
    [SerializeField] Transform boundaryBottomLeft; 

    private Vector3 minBoundary;
    private Vector3 maxBoundary;

    private void Start()
    {
        minBoundary = boundaryBottomLeft.position;
        maxBoundary = boundaryTopRight.position;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = transform.position;

            newPosition.x = Mathf.Clamp(target.position.x, minBoundary.x, maxBoundary.x);
            newPosition.y = Mathf.Clamp(target.position.y, minBoundary.y, maxBoundary.y);

            transform.position = newPosition;
        }
    }
}
