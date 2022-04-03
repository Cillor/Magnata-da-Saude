using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterCreator : MonoBehaviour{

    SaveState state;

    public TMP_Text activityText, weightText, sleepText;
    public TMP_Text height;
    public TMP_InputField age;

    private void Start() {
        state = SaveManager.Instance.state;
        if(state.characterCreated){
            SceneManager.LoadScene(2);
        }
        age.text = state.ageYears + "";
    }

    public void GetHeight(float _h){
        height.text = _h + "cm";
        state.heightCentimeters = (int) _h;
        GetDifficulty(state.difficulty);
    }

    public void GetAge(string input){
        int _a = int.Parse(input);
        if(13 <= _a && _a <= 70){
            state.ageYears = _a;
        }else{
            age.text = state.ageYears + "";
        }
    }

    public void GetSex(int index){
        if(index == 0){
            state.sexFactor = -161;
        }else{
            state.sexFactor = 5;
        }
    }

    public void GetDifficulty(int index){
        state.difficulty = index;
        switch(index){
            case 0: //facil
                state.activityFactor = 1.2f;
                state.currentWeightKg = 30 * Mathf.Pow(state.heightCentimeters/100f, 2);
                state.numberOfSleeps = 50;
                state.totalHoursSlept = state.numberOfSleeps * 7;
                state.sleepQuality = 0.84f;
                state.bgp = 120;
                
                activityText.text = "Condicionamento: baixo";
                break;
            case 1: //normal
                state.activityFactor = 1.1f;
                state.currentWeightKg = 33 * Mathf.Pow(state.heightCentimeters/100f, 2);
                state.numberOfSleeps = 70;
                state.totalHoursSlept = state.numberOfSleeps * 6.5f;
                state.sleepQuality = 0.8f;
                state.bgp = 150;
                activityText.text = "Condicionamento: pouco";
                break;
            case 2: //dificil
                state.activityFactor = 1f;
                state.currentWeightKg = 37 * Mathf.Pow(state.heightCentimeters/100f, 2);
                state.numberOfSleeps = 100;
                state.totalHoursSlept = state.numberOfSleeps * 6;
                state.sleepQuality = 0.7f;
                state.bgp = 180;
                activityText.text = "Condicionamento: nenhum";
                break;
        }
        weightText.text = "Peso: " + state.currentWeightKg.ToString("00.00") + "kg";
        sleepText.text = "Sono: ~" + state.totalHoursSlept/state.numberOfSleeps + "hrs";
    }

    public void CreateCharacter(){
        state.characterCreated = true;
        SceneManager.LoadScene(2);
    }

}
