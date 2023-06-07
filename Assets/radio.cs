using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radio : MonoBehaviour
{
    public playerInteract key;
    public bool on;

    public float Requency;
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
        }
        catch (System.Exception)
        {
            Debug.Log("Player not loaded!");
        }


        if (key.ObjLooking == "radio" && Input.GetKeyDown(KeyCode.E))
        {
            on = !on;
        }


        if (on)
        {

        }
        else if (!on)
        {

        }

    }


    public void changeValue(float value)
    {
        Requency = value;
    }
}