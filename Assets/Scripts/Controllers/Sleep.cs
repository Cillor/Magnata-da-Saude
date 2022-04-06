using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Sleep : MonoBehaviour
{
    public GameObject sleepSelectionScreen;
    public TMP_Text amountOfHoursToSleepText, wakeUpHourAndMinuteText;

    float amountOfSleepWantedInHours;
    SaveState state;
    Clock clock;
    Energy energy;

    private void Start() {
        clock = GameObject.FindWithTag("clock").GetComponent<Clock>();
        energy = GameObject.FindWithTag("energy").GetComponent<Energy>();
        state = SaveManager.Instance.state;
        amountOfHoursToSleepText.text = TimeSpan.FromHours(1).ToString("hh'hrs'mm'min'");
        wakeUpHourAndMinuteText.text = clock.date.AddHours(1).ToString("HH:mm");
        Clock.OnDayChange += NewDay;
        Clock.OnHourChange += HourlyUpdate;
    }

    void NewDay(){
        state.numberOfSleeps++;
    }

    void HourlyUpdate(){
        state.hoursSinceLastSlept++;
    }

    public void ChangeAmountOfSleep(float value){
        amountOfSleepWantedInHours = value;
        TimeSpan time = TimeSpan.FromHours(amountOfSleepWantedInHours);
        amountOfHoursToSleepText.text = time.ToString("hh'hrs'mm'min'");
        DateTime date = clock.date.AddHours(amountOfSleepWantedInHours);
        wakeUpHourAndMinuteText.text = date.ToString("HH:mm");
    }

    public void SleepAction(){
        int sleepStartHour = clock.date.Hour;

        //checks for avoiding sleep during school time
        if(clock.AddTime(amountOfSleepWantedInHours) == 0){
            Debug.Log("Cannot sleep");
            return;
        }
        sleepSelectionScreen.SetActive(false);

        state.totalHoursSlept += amountOfSleepWantedInHours; //add hours slept

        //calculates sleep quality
        float sleepAverage = state.totalHoursSlept/state.numberOfSleeps;
        state.sleepQuality += (-Mathf.Pow(0.5f * (sleepAverage - 8), 2) + 1)/100f;

        //calculates when the player will wake up the next time the sleep screen opens
        DateTime date = clock.date.AddHours(amountOfSleepWantedInHours); 
        wakeUpHourAndMinuteText.text = date.ToString("HH:mm");

        //refills player energy based on amount of time slept
        float thisSleepQuality = Mathf.Cos(0.27f * (sleepStartHour - 22));
        float energyIncreaseValue = UnityEngine.Random.Range(0.02f, 0.05f);
        energyIncreaseValue *= amountOfSleepWantedInHours * thisSleepQuality;
        energy.ChangeEnergy(energyIncreaseValue);

        //changes Sleep deficit
        state.hoursSinceLastSlept -= (amountOfSleepWantedInHours)*2.2f;
        
        if(state.hoursSinceLastSlept <= 0)
            state.hoursSinceLastSlept = 0;
        
        if(state.hoursSinceLastSlept > (24*5))
            SceneManager.LoadScene("SleepDeath");
    }
}
