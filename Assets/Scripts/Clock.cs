using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour{
    public DateTime date = new DateTime(2022, 1, 1, 0, 0, 0);

    public TMP_Text clockText;

    private int currentDay;

    private void Start() {
        date = new DateTime(SaveManager.Instance.state.date[0], SaveManager.Instance.state.date[1], SaveManager.Instance.state.date[2], 
                    SaveManager.Instance.state.date[3], SaveManager.Instance.state.date[4], SaveManager.Instance.state.date[5]);
        
        currentDay = date.Day;
        clockText.text = date.ToString("MM/dd/yyyy HH:mm");
    }

    public void AddTime(float hours){
        date = date.AddHours(hours);
        clockText.text = date.ToString("MM/dd/yyyy HH:mm");

        if(currentDay != date.Day){
            
        }
    }
}
