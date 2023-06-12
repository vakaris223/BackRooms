﻿using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]

    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;
    public bool on = true;

    Vector2 velocity;
    Vector2 frameVelocity;



    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        on = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

            // Get smooth velocity.
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;

            velocity.y = Mathf.Clamp(velocity.y, -70, 70);
            
            if(on)
            {
                // Rotate camera up-down and controller left-right from velocity.
                transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
                character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
            }

        

    }
}
