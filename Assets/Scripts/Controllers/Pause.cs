using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseScreen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            isPaused = !isPaused;
        }

        pauseScreen.SetActive(isPaused);
    }

    public void Return(){
        isPaused = false;
    }
}
