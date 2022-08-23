using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clok : MonoBehaviour{
    int clockIndex = 1;
    float[] speeds = {0.25f, 0.5f, 1f, 2f, 4f, 8f, 16f};
    public float clockSpeed;


    public DateTime date = new DateTime(2022, 1, 1, 0, 0, 0);

    public TMP_Text mainClockText, bedClockText, gymClockText, computerClockText, clockSpeedText;

    public static bool timeStopped = false;

    private int currentDay, currentHour;

    SaveState state = SaveManager.Instance.state;

    public delegate void DayChangeAction();
    public static event DayChangeAction OnDayChange;
    
    public delegate void HourChangeAction();
    public static event HourChangeAction OnHourChange;
    private void Start() {
        date = new DateTime(state.date[0], state.date[1], state.date[2], state.date[3], state.date[4],state.date[5]);
        currentDay = date.Day;
        currentHour = date.Hour;
        UpdateClocks();

        ChangeClockSpeed();
    }

    float timeCounter;
    private void Update() {
        if(Pause.isPaused)
            return;

        if(timeStopped)
            return;

        timeCounter += Time.deltaTime;

        if(timeCounter > 1/clockSpeed){
            timeCounter = 0;
            AddTime(1/60f);
        }

        if(currentDay != date.Day){
            currentDay = date.Day;
            if(OnDayChange != null)
                OnDayChange();
        }
        if(currentHour != date.Hour){
            currentHour = date.Hour;
            if(OnHourChange != null)
                OnHourChange();
        }
    }

    public void UpdateClocks(){
        mainClockText.text = date.ToString("dd/MM/yyyy HH:mm");
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

        state.date = new int[]{date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second};

        for(int i = 0; i < Mathf.FloorToInt(hours); i++)
            OnHourChange();

        UpdateClocks();
        return 1;
    }

    public void PauseClock(){
        timeStopped = !timeStopped;
        if(timeStopped)
            clockSpeedText.text = "||";
        else
            Display();
    }

    public void ChangeClockSpeed(){
        timeStopped = false;
        clockIndex++;
        clockIndex %= speeds.Length;
        clockSpeed = speeds[clockIndex];

        Display();
    }

    void Display(){
        if(clockSpeed <= 1)
            clockSpeedText.text = "► " + (clockSpeed).ToString("0.00") + "x";
        else
            clockSpeedText.text = "►► " + (clockSpeed).ToString("0") + "x";
    }
}
