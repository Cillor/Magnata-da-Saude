using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour{
    public float clockSpeed = 3f;
    public DateTime date = new DateTime(2022, 1, 1, 0, 0, 0);

    public TMP_Text mainClockText, kitchenClockText, bedClockText, gymClockText, computerClockText;

    public static bool timeStopped = false;

    private int currentDay;

    SaveState state = SaveManager.Instance.state;

    public delegate void DayChangeAction();
    public static event DayChangeAction OnDayChange;
    
    private void Start() {
        date = new DateTime(state.date[0], state.date[1], state.date[2], state.date[3], state.date[4],state.date[5]);
        currentDay = date.Day;
        UpdateClocks();

        OnDayChange();
    }

    float timeCounter;
    private void Update() {
        if(timeStopped)
            return;

        timeCounter += Time.deltaTime;

        if(timeCounter > clockSpeed){
            timeCounter = 0;
            AddTime(1/60f);
        }
    }

    public void UpdateClocks(){
        mainClockText.text = date.ToString("MM/dd/yyyy HH:mm");
        bedClockText.text = date.ToString("HH:mm");
        gymClockText.text = date.ToString("HH:mm");
        computerClockText.text = date.ToString("HH:mm");
    }

    public int AddTime(float hours){

        DateTime newDate = date.AddHours(hours);
        if(date.Hour <= 17 && newDate.Hour >= 13){
            Debug.Log("Cannot pass time, I need to go to school");
            return 0;
        }
        date = date.AddHours(hours);
        UpdateClocks();
        
        if(currentDay != date.Day){
            currentDay = date.Day;
            if(OnDayChange != null)
                OnDayChange();
        }
        return 1;
    }
}
