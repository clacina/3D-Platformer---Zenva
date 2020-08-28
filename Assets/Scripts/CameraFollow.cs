using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target = null;
    public Vector3 offset;

    private void Start()
    {
        offset.x = 0.0f;
        offset.y = 8.0f;
        offset.z = -10.0f;
    }

    private void Update()
    {
        if(target == null)
        {
            return;
        }

        Vector3 newPos = target.position + offset;
        //newPos.z = offset.z;

        transform.position = newPos;
    }
}
