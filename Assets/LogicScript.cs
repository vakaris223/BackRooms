using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LogicScript : MonoBehaviour
{

    public RandomMovement monster;
    public Text distancePtM;

    public playerInteract Interact;
    public Text ObjLookingAt;


    public float randomNum;
    public Text randomNumTXT;



    // Start is called before the first frame update
    void Start()
    {
        randomNum = Random.Range(87f, 180f);

        randomNum = Mathf.Round(randomNum * 100.0f) * 0.01f;
        randomNumTXT.text = randomNum.ToString();
        // Frequency = Mathf.Round(slider.value * 100.0f) * 0.01f;
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
