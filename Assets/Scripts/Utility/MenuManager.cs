using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour{
    void Start(){
        PlayerPrefs.SetInt("playTutorial", 1);
    }

    public void ResetSave(){
        SaveManager.Instance.ResetSave();
    }

    public void PlayTutorial(bool play){
        PlayerPrefs.SetInt("playTutorial", play ? 1 : 0);
    }
}
