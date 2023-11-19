using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class locker : MonoBehaviour
{

    private Animator anim;
    private playerInteract key;
    public bool open;
    private GameObject door;

    public RandomMovement monster;

    // Start is called before the first frame update
    void Start()
    {
        open = false;
        anim = GetComponent<Animator>();
        door = gameObject.transform.GetChild(0).gameObject;
        

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

        try
        {
            monster = GameObject.FindGameObjectWithTag("monster").GetComponent<RandomMovement>();
        }
        catch (System.Exception)
        {
            Debug.Log("Monster not founded!");
        }

        //key.ObjLooking == "locker"
        try
        {
            if (Input.GetKeyDown(KeyCode.E) && key.hit.collider.gameObject == gameObject)
            {
                open = !open;
            }
            else if (Input.GetKeyDown(KeyCode.E) && key.hit.collider.gameObject == door)
            {
                open = !open;
            }

        }
        catch (System.Exception)
        {

            Debug.Log("hit.collider.gameObject == N/A");
        }
            


        

       

        if (open)
            anim.SetBool("open", true);
        else if (!open)
            anim.SetBool("open", false);


    }


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            monster.canSee = false;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            monster.canSee = true;
        }
    }



}
