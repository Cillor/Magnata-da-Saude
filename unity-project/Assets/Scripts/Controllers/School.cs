using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : MonoBehaviour{
    Timer.Clock clock;
    Timer.UI.Clock clockUI;
    Energy energy;
    SaveState state = SaveManager.Instance.state;

    public GameObject goToSchoolScreen;
    public GameObject[] screens;

    private void Start() {
        clock = FindObjectOfType<Timer.Clock>();
        clockUI = FindObjectOfType<Timer.UI.Clock>();
        energy = FindObjectOfType<Energy>();
    }

    void Update(){
        if(Pause.isPaused)
            return;
            
        if(clock.Date.Hour == 12 && clock.Date.Minute == 59){
            clock.Date = clock.Date.AddMinutes(1);
            clockUI.UpdateGameClocks();
            Timer.Clock.timeStopped = true;

            goToSchoolScreen.SetActive(true);
        }
    }

    public void GoToSchool(){
        Timer.Clock.timeStopped = false;
        Debug.Log("THIS BAD BOY RUNS");
        clock.SetHours(18);
        Debug.Log("AND HE UPDATES THE TIME");
        clockUI.UpdateGameClocks();
        Debug.Log("AND HE UPDATES THE FREAKING CLOCK!!");

        float energyDecreaseValue = UnityEngine.Random.Range(-0.2f, -0.05f);
        energy.ChangeEnergy(energyDecreaseValue);
        foreach(GameObject go in screens){
            go.SetActive(false);
        }

        //drains player energy
    }
}
