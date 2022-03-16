using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sleep : MonoBehaviour
{
    public GameObject sleepSelectionScreen;
    public TMP_Text currentHourAndMinuteText;
    public TMP_Text amountOfHoursToSleepText;
    public TMP_Text wakeUpHourAndMinuteText;


    Clock clock;

    float amountOfSleepWantedInHours;
    SaveState state;

    private void Start() {
        clock = GameObject.FindWithTag("clock").GetComponent<Clock>();
        state = SaveManager.Instance.state;
        currentHourAndMinuteText.text = clock.date.ToString("HH:mm");
        amountOfHoursToSleepText.text = TimeSpan.FromHours(1).ToString("hh'hrs'mm'min'");
        wakeUpHourAndMinuteText.text = clock.date.AddHours(1).ToString("HH:mm");
    }

    public void ChangeAmountOfSleep(float value){
        amountOfSleepWantedInHours = value;
        TimeSpan time = TimeSpan.FromHours(amountOfSleepWantedInHours);
        amountOfHoursToSleepText.text = time.ToString("hh'hrs'mm'min'");
        DateTime date = clock.date.AddHours(amountOfSleepWantedInHours);
        wakeUpHourAndMinuteText.text = date.ToString("HH:mm");
    }

    public void SleepAction(){
        if(clock.AddTime(amountOfSleepWantedInHours) == 0){
            Debug.Log("Cannot sleep");
            return;
        } //advances time
        currentHourAndMinuteText.text = clock.date.ToString("HH:mm"); //changes the current time inside the sleep window
        sleepSelectionScreen.SetActive(false);

        state.totalHoursSlept += amountOfSleepWantedInHours;
        state.numberOfSleeps++;

        float sleepAverage = state.totalHoursSlept/state.numberOfSleeps;
        state.sleepQuality = -Mathf.Pow(0.4f * (sleepAverage - 8), 2) + 1;

        //calculates when the player will wake up the next time the sleep screen opens
        DateTime date = clock.date.AddHours(amountOfSleepWantedInHours); 
        wakeUpHourAndMinuteText.text = date.ToString("HH:mm");
    }
}
