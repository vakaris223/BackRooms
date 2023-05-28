using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : Interactable
{
    protected override void Interact()
    {
        Destroy(gameObject);
    }
    protected override void PickUp(GameObject player, Camera cam, bool picked, bool overload)
    {
        if(picked)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.gameObject.GetComponent<MeshCollider>().isTrigger = true;
            this.gameObject.transform.parent = cam.transform;
            overload = true;
        }
        else if (!picked)
        {
            if (cam.transform.Find(this.gameObject.name))
            {
                Transform obj = cam.transform.Find(this.gameObject.name);
                this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                this.gameObject.GetComponent<MeshCollider>().isTrigger = false;
                obj.transform.parent = null;
            }
        }

        

        
        //new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 1);
    }

    /* protected override void Throw(GameObject player, Camera cam) 
     {
         if (cam.transform.Find(this.gameObject.name))
         {
             Transform obj = cam.transform.Find(this.gameObject.name);
             this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
             this.gameObject.GetComponent<MeshCollider>().isTrigger = false;
             obj.transform.parent = null;
             this.gameObject.GetComponent<Rigidbody>().velocity += Vector3.forward;
         }

     }*/
}
