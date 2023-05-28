using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public Camera cam;
    private float xRot = 0f;

    public float xSens = 30f;
    public float ySens = 30f;

   public void ProcessLook(Vector2 input, bool lockMouse)
   {
        if(lockMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        float mouseX = input.x;
        float mouseY = input.y;

        //calculate rotation with mouse Vector2
        xRot -= (mouseY * Time.deltaTime) * ySens;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        //apply this to camera
        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        //rotate player to look left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSens);
   }
}
