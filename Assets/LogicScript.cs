using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{

    public RandomMovement monster;
    public Text distancePtM;

    public playerInteract Interact;
    public Text ObjLookingAt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        try
        {
            monster = GameObject.FindGameObjectWithTag("monster").GetComponent<RandomMovement>();
            distancePtM.text = monster.distance.ToString();
        }
        catch (System.Exception)
        {
            Debug.Log("Monster prefab not loaded!");
        }

        try
        {
            Interact = GameObject.FindGameObjectWithTag("Player").GetComponent<playerInteract>();
            ObjLookingAt.text = Interact.ObjLooking;
        }
        catch (System.Exception)
        {

            Debug.Log("Player not loaded!");
        }
       
;    }
}
