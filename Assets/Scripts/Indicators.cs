using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Indicators : MonoBehaviour{

    SaveState state = SaveManager.Instance.state;

    public TMP_Text weightText, bmiText, restingHeartRateText, bloodPressureText, a1cText;

    // Update is called once per frame
    void Update()
    {
        weightText.text = "Peso: " + state.currentWeightKg.ToString("0.00") + "kg";
        bmiText.text = "IMC: " + state.bmi.ToString("0.00");
        restingHeartRateText.text = "FCD: " + state.restingHeartRate;
    }
}
