using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.04f;

    private void Start() {
        offset = transform.position - target.position;
    }

    private void FixedUpdate() {
        FollowHelixBall();
    }

    private void FollowHelixBall(){
        
        Vector3 newPos = Vector3.Lerp (transform.position, target.position + offset, smoothSpeed);
        transform.position = newPos;
    }
}
