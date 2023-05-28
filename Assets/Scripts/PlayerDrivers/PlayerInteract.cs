using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUi playerUi;
    private InputManager inputManager;



    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUi = GetComponent<PlayerUi>();
        inputManager = GetComponent<InputManager>();
    }


    // Update is called once per frame
    void Update()
    {
        playerUi.UpdateText(string.Empty);
        //creates an invisable line at the center of a camera, shooting outwards
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.black);
        RaycastHit hitInfo;


        //RaycastHit hitInfos;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {

            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {

                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                
                playerUi.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
                if(interactable.overload)
                {
                    if (inputManager.onFoot.PickUp.inProgress)
                    {
                        interactable.BasePickUp(true);
                    }
                    else if (!inputManager.onFoot.PickUp.inProgress)
                    {
                        interactable.BasePickUp(false);
                    }
                }
               
                /*if (inputManager.onFoot.Throw.triggered)
                {
                    interactable.BaseThrow();
                    Debug.Log("Thrown");
                }*/


            }


        }
    }
}
