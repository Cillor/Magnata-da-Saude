using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour{
    public DateTime date = new DateTime(2022, 1, 1, 0, 0, 0);

    public TMP_Text clockText;

    private void Start() {
        date = new DateTime(PlayerData.date[0], PlayerData.date[1], PlayerData.date[2], PlayerData.date[3], PlayerData.date[4], PlayerData.date[5]);
        clockText.text = date.ToString("MM/dd/yyyy HH:mm");
    }

    public void AddTime(float hours){
        date = date.AddHours(hours);
        clockText.text = date.ToString("MM/dd/yyyy HH:mm");
    }
}
