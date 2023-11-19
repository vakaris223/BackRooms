using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenesMG : MonoBehaviour
{
    float timer = 5; // 10 seconds
    bool stopped = false;
    private void Update()
    {
        if (timer <= 0 && !stopped)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            timer -= Time.deltaTime;
        }
            
    }

}
