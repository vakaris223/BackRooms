using UnityEngine;
using UnityEngine.UI;

public class radio : MonoBehaviour
{
    public float Frequency;

    public Slider slider;
    public Text FrequencyText;
    public GameObject panel;
    

    private playerInteract key;
    private Rigidbody player;
    private FirstPersonLook playerL;

    private bool on;
    private float previousSliderValue;

    private LogicScript lg;

    public AudioSource[] voices;
    public AudioSource whitenoise;
    public AudioSource talkingRadio;

    public AudioLowPassFilter[] clips;

    public Material mat;
    private int rnd;

    public int rightFR;

    public bool MeetsCondition()
    {
        // Add your condition logic here
        // For example, checking if rightFR is equal to 1
        return (rightFR == 1);
    }
    private void Start()
    {
        on = false;

        key = GameObject.FindGameObjectWithTag("Player").GetComponent<playerInteract>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        playerL = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonLook>();
        lg = GameObject.Find("Logic").GetComponent<LogicScript>();

        // Deactivate the panel initially
        panel.SetActive(false);

        // Store the initial slider value
        previousSliderValue = slider.value;

        for (int i = 0; i < voices.Length; i++)
        {
            voices[i].volume = 0f;
        }

        rnd = Random.Range(0, 1);

    }

    private void Update()
    {
        // Check if the player interacts with the radio object
        if (key.hit.collider != null && key.hit.collider.gameObject == gameObject && Input.GetKeyDown(KeyCode.E))
        {
            on = !on;

            if (on)
            {
                player.isKinematic = true;
                playerL.on = false;
                panel.SetActive(true);
            }
            else
            {
                player.isKinematic = false;
                playerL.on = true;
                panel.SetActive(false);
            }
        }

        // Update the frequency value and display it on the frequency text
        Frequency = Mathf.Round(slider.value * 100.0f) * 0.01f;
        FrequencyText.text = Frequency.ToString();

        if (slider.value >= lg.randomNum && slider.value <= lg.randomNum + 10)
        {
            rightFR = 1;
            talkingRadio.volume = 0.3f;
            whitenoise.volume = 0.1f;
            
        }
        else if (slider.value >= lg.randomNumVoice && slider.value <= lg.randomNumVoice + 5)
        {
            voices[rnd].volume = 0.2f;
            whitenoise.volume = 0.4f;
            talkingRadio.volume = 0f;
            rightFR = 0;
        }
        else
        {
            rightFR = 0;
            voices[rnd].volume = 0f;
            talkingRadio.volume = 0f;
            whitenoise.volume = 0.4f;
        }


        // Store the current slider value for the next frame
        previousSliderValue = slider.value;

        Vector3 direction = player.transform.position - transform.position;

        Ray ray = new Ray(transform.position, direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hit the player
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                for (int i = 0; i < clips.Length; i++)
                {
                    clips[i].cutoffFrequency = 5007.7f;
                }
                Debug.Log("Player hit!");
            }
            else
            {
                for (int i = 0; i < clips.Length; i++)
                {
                    clips[i].cutoffFrequency = 2000f;
                }
            }
        }
    }

    public void Exit()
    {
        on = false;
        player.isKinematic = false;
        playerL.on = true;
        panel.SetActive(false);
    }
}
