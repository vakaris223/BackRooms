using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //message to tell to player when he is looking at an interactable object
    public string promptMessage;

    
    public GameObject player;
    public Camera cam;

    public bool overload;


    //fuction that will be called from player
    public void BaseInteract()
    {
        Interact();
    }
    protected virtual void Interact()
    {
        //we won't have any code written in this function
        //this is a templete function to be overridden by out subclasses ("clones")(it work like a framework)
    }

    public void BasePickUp(bool picked)
    {
        PickUp(player, cam, picked, overload);
    }
    protected virtual void PickUp(GameObject player, Camera cam, bool picked, bool overload) { }


   /* public void BaseThrow()
    {
        Throw(player, cam);
    }
    protected virtual void Throw(GameObject player, Camera cam) { }*/

}
