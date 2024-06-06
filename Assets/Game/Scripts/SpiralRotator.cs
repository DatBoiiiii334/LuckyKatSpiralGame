using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralRotator : MonoBehaviour
{
    public float rotationSpeedPC;
    public float rotationSpeedDevice;

    private void Update() {

        // PC Controls
        #if UNITY_EDITOR
            if(Input.GetMouseButton(0)){
                float mouseX = Input.GetAxisRaw("Mouse X");
                transform.Rotate (transform.position.x, -mouseX * rotationSpeedPC * Time.deltaTime, transform.position.z);
            }
        
        // Android/Iphone Controls
        #elif UNITY_ANDROID
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            float xDeltaPos = Input.GetTouch(0).deltaPosition.x;
            transform.Rotate(transform.position.x, -xDeltaPos * rotationSpeedDevice * Time.deltaTime, transform.position.z);
        }
        #endif
    }
}
