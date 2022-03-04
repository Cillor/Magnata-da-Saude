using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour{
    public DateTime date = new DateTime(2022, 1, 1, 0, 0, 0);

    public TMP_Text clockText;

    private void Start() {
        date = new DateTime(SaveState.date[0], SaveState.date[1], SaveState.date[2], SaveState.date[3], SaveState.date[4], SaveState.date[5]);
        clockText.text = date.ToString("MM/dd/yyyy HH:mm");
    }

    public void AddTime(float hours){
        date = date.AddHours(hours);
        clockText.text = date.ToString("MM/dd/yyyy HH:mm");
    }
}
