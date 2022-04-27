using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Message{
    public string text;
    public Color color;

    public Message(string _t, Color _c){
        this.text = _t;
        this.color = _c;
    }
}

public class Indicators : MonoBehaviour{

    SaveState state = SaveManager.Instance.state;

    public TMP_Text weightText, bmiText, restingHeartRateText, glycatedHemoglobinText, caloriesText;

    public Image[] imagesPieChart;
    public TMP_Text[] pieChartText;

    [Space]
    public GameObject popupHolder;
    public GameObject popupTemplate;

    List<Message> messages = new List<Message>();

    void Update(){
        weightText.text = "Peso: " + state.currentWeightKg.ToString("0.00") + "kg";
        bmiText.text = "IMC: " + state.bmi.ToString("0.00");
        restingHeartRateText.text = "FCD: " + state.restingHeartRate;
        glycatedHemoglobinText.text = "GS: " + state.bgp + "mg/dL";


        caloriesText.text = state.calorieDifference + "kcal";
        float[] pieChartValues = new float[3]{state.protein, state.fat, state.carbs};
        SetValues(pieChartValues);

        while(messages.Count > 0){
            PopUpMessage(messages[0]);
            messages.RemoveAt(0);
        }
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

    public void AddMessage(string message, Color color){
        Message _m = new Message(message, color);
        messages.Add(_m);
    }

    void PopUpMessage(Message _m){
        GameObject _popup = Instantiate(popupTemplate, popupHolder.transform);
        TMP_Text messagePopup = _popup.GetComponent<TMP_Text>();
        messagePopup.text = _m.text;
        messagePopup.color = _m.color;
        StartCoroutine(HideMessage(messagePopup));
    }

    IEnumerator HideMessage(TMP_Text messagePopup){
        yield return new WaitForSeconds(4);
        GameObject.Destroy(messagePopup.gameObject);
    }
}
