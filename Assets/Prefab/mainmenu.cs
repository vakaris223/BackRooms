using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class mainmenu : MonoBehaviour
{
    
    public Button Play;
    public Button Quit;

    public void play()
    {
        SceneManager.LoadScene(1);
    }
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void quit()
    {
        Application.Quit();
    }



}
