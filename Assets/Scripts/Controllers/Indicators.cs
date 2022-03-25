using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Indicators : MonoBehaviour{

    SaveState state = SaveManager.Instance.state;

    public TMP_Text weightText, bmiText, restingHeartRateText, bloodPressureText, a1cText, caloriesText;

    public Image[] imagesPieChart;
    public TMP_Text[] pieChartText;


    // Update is called once per frame
    void Update()
    {
        weightText.text = "Peso: " + state.currentWeightKg.ToString("0.00") + "kg";
        bmiText.text = "IMC: " + state.bmi.ToString("0.00");
        restingHeartRateText.text = "FCD: " + state.restingHeartRate;
        a1cText.text = "GS: " + state.bgp + "mg/dL";


        caloriesText.text = state.calorieDifference + "kcal";
        float[] pieChartValues = new float[3]{state.protein, state.fat, state.carbs};
        SetValues(pieChartValues);
    }

    public void SetValues(float[] valuesToSet){
        float totalValues = 0;
        for(int i = 0; i < imagesPieChart.Length; i++){
        totalValues += FindPercentage(valuesToSet, i);
        pieChartText[i].text = valuesToSet[i].ToString("(0g)") + " - " + FindPercentage(valuesToSet, i).ToString("0%");
        imagesPieChart[i].fillAmount = totalValues;
        }
    }

    private float FindPercentage(float[] valueToSet, int index){
        float totalAmount = 0;
        for(int i = 0; i< valueToSet.Length; i++){
        totalAmount += valueToSet[i];
        }
        return valueToSet[index] / totalAmount;
    }
}
