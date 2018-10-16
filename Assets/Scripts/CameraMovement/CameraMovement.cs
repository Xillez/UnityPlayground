using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform target;
    public Vector3 offset;
    public float dampingMultiplier;
    
    void LateUpdate()
    {
        if (!target)
            return;

        transform.position = Vector3.Slerp(transform.position, target.position + offset, dampingMultiplier * Time.deltaTime);
                        
        transform.LookAt(target);
    }
}
