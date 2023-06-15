using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class radio : MonoBehaviour
{

    public radioInterct intr;
    public Slider slider;
    public float Frequency;
    public Text FrequencyText;
    public GameObject panel;
    private FirstPersonLook fpl;
    private FirstPersonMovement fpm;
    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.FindGameObjectWithTag("panel");
        FrequencyText = GameObject.FindGameObjectWithTag("frequency").GetComponent<Text>();
        slider = GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>();
        fpl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonLook>();
        fpm = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        try
        {
            intr = GameObject.FindGameObjectWithTag("radio").GetComponent<radioInterct>();
        }
        catch (System.Exception)
        {
            Debug.Log("radio not loaded!");
        }

        if (intr.on)
        {
            panel.SetActive(true);
            Frequency = Mathf.Round(slider.value * 100.0f) * 0.01f;
            FrequencyText.text = Frequency.ToString();
        }
        else if (!intr.on)
        {
            panel.SetActive(false);
        }
    }

    public void exit()
    {
        intr.on = false;

    }
}