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

        if(clock.AddTime(amountOfSleepWantedInHours) == 0){
            Debug.Log("Cannot sleep");
            return;
        } //advances time
        sleepSelectionScreen.SetActive(false);

        state.totalHoursSlept += amountOfSleepWantedInHours;

        float sleepAverage = state.totalHoursSlept/state.numberOfSleeps;
        state.sleepQuality = -Mathf.Pow(0.4f * (sleepAverage - 8), 2) + 1;

        //calculates when the player will wake up the next time the sleep screen opens
        DateTime date = clock.date.AddHours(amountOfSleepWantedInHours); 
        wakeUpHourAndMinuteText.text = date.ToString("HH:mm");

        //refills player energy based on amount of time slept
        float sleepQuality = Mathf.Cos(0.27f * (sleepStartHour - 22));
        float energyIncreaseValue = UnityEngine.Random.Range(0.02f, 0.05f);
        energyIncreaseValue *= amountOfSleepWantedInHours * sleepQuality;
        energy.ChangeEnergy(energyIncreaseValue);

        //changes Sleep deficit
        state.hoursSinceLastSlept -= (amountOfSleepWantedInHours)*2.2f;
        
        if(state.hoursSinceLastSlept <= 0)
            state.hoursSinceLastSlept = 0;
        
        if(state.hoursSinceLastSlept > (24*5))
            SceneManager.LoadScene("SleepDeath");
    }

    //TODO Hours without sleeping
}
