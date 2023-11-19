using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioM : MonoBehaviour
{

    public AudioClip Crack;
    public AudioClip scream;
    public AudioSource audioSourceCrack;
    public AudioSource audioSourcescream;


    public float time;

    void Start()
    {
        Invoke("PlayCrack", time);

        Invoke("PlayScream", 0.5f);
    }

    void PlayCrack()
    {
        audioSourceCrack.clip = Crack;
        audioSourceCrack.Play();
    }

    void PlayScream()
    {
        audioSourcescream.clip = scream;
        audioSourcescream.Play();
    }
}
