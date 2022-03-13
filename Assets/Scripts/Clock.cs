using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour{
    public DateTime date = new DateTime(2022, 1, 1, 0, 0, 0);

    public TMP_Text mainClockText, kitchenClockText, bedClockText, gymClockText;

    private int currentDay;

    SaveState state = SaveManager.Instance.state;

    public delegate void DayChangeAction();
    public static event DayChangeAction OnDayChange;
    
    private void Start() {
        date = new DateTime(state.date[0], state.date[1], state.date[2], state.date[3], state.date[4],state.date[5]);
        currentDay = date.Day;
        mainClockText.text = date.ToString("MM/dd/yyyy HH:mm");
        bedClockText.text = date.ToString("HH:mm");
        gymClockText.text = date.ToString("HH:mm");
        Debug.Log(state.ageYears);
        OnDayChange();
    }

    public void AddTime(float hours){
        date = date.AddHours(hours);
        mainClockText.text = date.ToString("MM/dd/yyyy HH:mm");
        bedClockText.text = date.ToString("HH:mm");
        gymClockText.text = date.ToString("HH:mm");
        
        if(currentDay != date.Day){
            currentDay = date.Day;
            if(OnDayChange != null)
                OnDayChange();
        }
    }
}
