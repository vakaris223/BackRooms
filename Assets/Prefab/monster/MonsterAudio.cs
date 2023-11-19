using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAudio : MonoBehaviour
{

    private RaycastHit hit;
    public AudioLowPassFilter[] AudioLowPassFilters;
    public AudioSource[] sources;
    public AudioClip[] clips;
    private GameObject player;
    public bool running;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(WaitAndPlay());
    }

    public IEnumerator WaitAndPlay()
    {
        while (true)
        {
            if(!running)
            {
                sources[1].PlayOneShot(clips[1]);
                yield return new WaitForSeconds(0.6f);
            }
            else if(running)
            {
                sources[0].PlayOneShot(clips[0]);
                yield return new WaitForSeconds(0.4f);
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hit the player
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                for (int i = 0; i < AudioLowPassFilters.Length; i++)
                {
                    AudioLowPassFilters[i].cutoffFrequency = 5007.7f;
                }
                Debug.Log("Player hit!");
            }
            else
            {
                for (int i = 0; i < AudioLowPassFilters.Length; i++)
                {
                    AudioLowPassFilters[i].cutoffFrequency = 2000f;
                }
            }

        }
    }
}
