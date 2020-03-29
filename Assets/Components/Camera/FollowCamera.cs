using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject target = null;

    [SerializeField]
    private Vector3 offset = Vector3.zero;

    [SerializeField]
    private float smoothSpeed = 0.25f;

    private void Update()
    {
        if(target != null)
        {
            Vector3 desiredPosition = target.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
