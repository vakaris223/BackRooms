using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerInteract : MonoBehaviour
{
    Camera cam;
    public string ObjLooking;
    public RaycastHit hit;

    public GameObject InteractUI;
    // Start is called before the first frame update
    
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        try
        {
            InteractUI = GameObject.Find("Active");
            //InteractUI.SetActive(false);
        }
        catch (System.Exception)
        {
            Debug.Log("Player prefab not loaded!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 7;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 0.6f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            ObjLooking = hit.collider.gameObject.tag;
            //Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            ObjLooking = "N/A";
        }

        if (ObjLooking == "radio" || ObjLooking == "locker")
        {
            InteractUI.SetActive(true);
        }
        else if (ObjLooking == "N/A" || ObjLooking == null)
        {
            InteractUI.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("monster"))
        {

            SceneManager.LoadScene(sceneBuildIndex:2);
        }
    }

}
