using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{

    public RandomMovement monster;
    public Text distancePtM;
    public Text MonsterLokingAt;

    public playerInteract Interact;
    public Text ObjLookingAt;

    public float randomNum;
    public Text randomNumTXT;

    public float randomNumVoice;
    public Text randomNumVoiceTXT;

    public int correctRadiosCount = 0;
    public Text correctRadiosCountText;



    void Start()
    {
        randomNum = Random.Range(87f, 180f);
        randomNum = Mathf.Round(randomNum * 100.0f) * 0.01f;
        randomNumTXT.text = randomNum.ToString();
        // Frequency = Mathf.Round(slider.value * 100.0f) * 0.01f;


        randomNumVoice = Random.Range(87f, 180f);
        randomNumVoice = Mathf.Round(randomNumVoice * 100.0f) * 0.01f;
        randomNumVoiceTXT.text = randomNumVoice.ToString();
        
    }


    // Update is called once per frame
    void Update()
    {
        CountRadiosWithCondition();


        try
        {
            monster = GameObject.FindGameObjectWithTag("monster").GetComponent<RandomMovement>();
            //MonsterLokingAt.text = monster.hit.collider.gameObject.name;
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

        if(GameObject.Find("Generator").GetComponent<MazeGenerator>().done)
        {
            if (correctRadiosCount == GameObject.Find("Generator").GetComponent<MazeGenerator>().countRadio)
            {
                SceneManager.LoadScene(3);
            }
        }
        

    }

    private void CountRadiosWithCondition()
    {
        // Find all radios in the scene
        GameObject[] radios = GameObject.FindGameObjectsWithTag("radio");

        // Initialize the counter
        int radiosMeetingCondition = 0;

        foreach (var radio in radios)
        {
            // Assuming that the radio script is attached to the GameObject
            radio radioScript = radio.GetComponent<radio>();

            // Check the condition in radio script
            if (radioScript.MeetsCondition())
            {
                radiosMeetingCondition++;
            }
        }

        // Update the count in LogicScript
        correctRadiosCount = radiosMeetingCondition;
        correctRadiosCountText.text = correctRadiosCount.ToString();
    }



}
