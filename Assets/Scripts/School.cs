using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : MonoBehaviour
{
    Clock clock;
    SaveState state = SaveManager.Instance.state;

    public GameObject goToSchoolScreen;

    private void Start() {
        clock = GameObject.FindWithTag("clock").GetComponent<Clock>();
    }

    void Update(){
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
        
        //drains player energy
    }
}
