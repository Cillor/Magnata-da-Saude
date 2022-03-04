using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Exercise : MonoBehaviour
{
    public GameObject exercisingScreen;

    public TMP_Text currentHourAndMinuteText;

    Clock clock;
    private void Start() {
        clock = GameObject.FindWithTag("clock").GetComponent<Clock>();
    }

    public void ExerciseAction(float hoursExercising){ //only exercises if energy allows it
        clock.AddTime(hoursExercising); //advances time
        currentHourAndMinuteText.text = clock.date.ToString("HH:mm"); //changes the current time inside the sleep window

        SaveManager.Instance.state.currentDayExerciseQuantity += 1;

        //energy expenditure based on amount of daily exercises and past week

        exercisingScreen.SetActive(false);
    }

    void DayChange(){
        for(int i = 0; i<6; i++){
            SaveManager.Instance.state.exerciseHistory[i] = SaveManager.Instance.state.exerciseHistory[i + 1];
        }
        SaveManager.Instance.state.exerciseHistory[6] = SaveManager.Instance.state.currentDayExerciseQuantity;
        SaveManager.Instance.state.currentDayExerciseQuantity = 0;

    float pastWeekExerciseSum =  SaveManager.Instance.state.exerciseHistory.Sum();

    SaveManager.Instance.state.activityFactor += (2 * Mathf.Cos(0.4f*pastWeekExerciseSum - Mathf.PI/2))/100f;
    SaveManager.Instance.state.activityFactor = Mathf.Clamp(SaveManager.Instance.state.activityFactor, 1, 2);

    SaveManager.Instance.state.activityFactor -= (-(1.4f*Mathf.Pow(SaveManager.Instance.state.activityFactor-2, 2))+2)/200f;
    SaveManager.Instance.state.activityFactor = Mathf.Clamp(SaveManager.Instance.state.activityFactor, 1, 2);
    }
}
