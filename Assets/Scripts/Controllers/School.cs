using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : MonoBehaviour
{
    Clock clock;
    Energy energy;
    SaveState state = SaveManager.Instance.state;

    public GameObject goToSchoolScreen;
    public GameObject[] screens;

    private void Start() {
        clock = GameObject.FindWithTag("clock").GetComponent<Clock>();
        energy = GameObject.FindWithTag("energy").GetComponent<Energy>();
    }

    void Update(){
        if(Pause.isPaused)
            return;
            
        if(clock.date.Hour == 12 && clock.date.Minute == 59){
            clock.date = clock.date.AddMinutes(1);
            clock.UpdateClocks();
            Clock.timeStopped = true;

            goToSchoolScreen.SetActive(true);
        }
    }

    public void GoToSchool(){
        Clock.timeStopped = false;
        clock.date = clock.date.AddHours(5);
        clock.UpdateClocks();

        float energyDecreaseValue = UnityEngine.Random.Range(-0.2f, -0.05f);
        energy.ChangeEnergy(energyDecreaseValue);
        foreach(GameObject go in screens){
            go.SetActive(false);
        }

        //drains player energy
    }
}
