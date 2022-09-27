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

    SaveState state;

    public TMP_Text weightText, bmiText, restingHeartRateText, caloriesText;

    [Space]
    public GameObject popupHolder;
    public GameObject popupTemplate;

    List<Message> messages = new List<Message>();

    private void Start() {
        state = SaveManager.Instance.state;
    }

    void Update(){
        weightText.text = "Peso: " + state.currentWeightKg.ToString("0.00") + "kg";
        bmiText.text = "IMC: " + state.bmi.ToString("0.00");
        restingHeartRateText.text = "FCD: " + state.restingHeartRate;

        while(messages.Count > 0){
            PopUpMessage(messages[0]);
            messages.RemoveAt(0);
        }
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
