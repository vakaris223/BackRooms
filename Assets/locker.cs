using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class locker : MonoBehaviour
{

    public Animator anim;
    public GameObject door;
    public playerInteract key;
    public bool open;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
        anim = GetComponent<Animator>();
        
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

        //key.ObjLooking == "locker"
        if (Input.GetKeyDown(KeyCode.E) && key.hit.collider.gameObject == this.gameObject) ;
        {
            open = !open;
        }


        if (open)
            anim.SetBool("open", true);
        else if (!open)
            anim.SetBool("open", false);









    }
}
