using System;
using System.Linq;
using UnityEngine;
using TMPro;

public class Computer : MonoBehaviour
{
    public TMP_Text timeStudyingResultText;

    Clock clock;

    SaveState state = SaveManager.Instance.state;


    private void Start() {
        clock = GameObject.FindWithTag("clock").GetComponent<Clock>();
    }

    public void StudyAction(){ //only exercises if energy allows it
        System.Random rnd = new System.Random();

        float hoursInTheComputer = (float)rnd.NextDouble() * (1.5f - 0.5f) + 0.5f;
        if(clock.AddTime(hoursInTheComputer) == 0){
            Debug.Log("Cannot Study");
            return;
        } //advances time
        
        int hoursHITC = Mathf.FloorToInt(hoursInTheComputer);
        int minutesHITC = Mathf.RoundToInt((hoursInTheComputer - hoursHITC) * 60);
        timeStudyingResultText.text = hoursHITC.ToString("00") + ":" + minutesHITC.ToString("00");

        //randomly gives or removes player energy
    }
}
