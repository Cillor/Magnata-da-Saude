using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseScreen;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            isPaused = !isPaused;
            pauseScreen.SetActive(isPaused);
        }

    }

    public void PauseGame(){
        isPaused = true;
    }
    public void UnPauseGame(){
        isPaused = false;
    }
}
