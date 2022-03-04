using System.Collections;
using System.Collections.Generic;
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

    public void ExerciseAction(float hoursExercising){
        clock.AddTime(hoursExercising); //advances time
        currentHourAndMinuteText.text = clock.date.ToString("HH:mm"); //changes the current time inside the sleep window
        exercisingScreen.SetActive(false);
    }
}
