using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radioInterct : MonoBehaviour
{
    
     public playerInteract key;
     public Rigidbody player;
     public FirstPersonLook playerL;
     public bool on;
     public float frequency;

    // Start is called before the first frame update
    void Start()
    {
        on = false;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            key = GameObject.FindGameObjectWithTag("Player").GetComponent<playerInteract>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            playerL = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonLook>();
        }
        catch (System.Exception)
        {
            Debug.Log("Player not loaded!");
        }

        if (key.ObjLooking == "radio" && Input.GetKeyDown(KeyCode.E))
        {
            on = !on;
            player.isKinematic = true;
            playerL.on = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(!on)
        {
            on = false;
            player.isKinematic = false;
            playerL.on = true;
            Cursor.lockState = CursorLockMode.Locked;

        }
        
    }
}
