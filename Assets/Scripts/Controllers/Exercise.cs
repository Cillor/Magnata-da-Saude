using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;

public class Exercise : MonoBehaviour{
    public TMP_Text exerciseResultsText;

    public GameObject resultsGO, exerciseButton;

    Clock clock;
    Energy energy;

    SaveState state = SaveManager.Instance.state;


    private void Start() {
        clock = GameObject.FindWithTag("clock").GetComponent<Clock>();
        energy = GameObject.FindWithTag("energy").GetComponent<Energy>();

        Clock.OnDayChange += WeightChange;
        Clock.OnDayChange += Heart;
    }

    public void ExerciseAction(){ //only exercises if energy allows it
        System.Random rnd = new System.Random();
        if(state.energy < UnityEngine.Random.Range(0.4f, 0.6f)){
            FindObjectOfType<Indicators>().AddMessage("Muito cansado para se exercitar", Color.red);
            FindObjectOfType<Indicators>().AddMessage("Durma ou se alimente para recuperar energia", Color.yellow);
            return;
        }

        float hoursExercising = UnityEngine.Random.Range(1f, 2.5f);

        if(SaveManager.Instance.state.tutorialCompleted){
            if(clock.AddTime(hoursExercising) == 0){
                FindObjectOfType<Indicators>().AddMessage("Sem tempo para se exercitar", Color.red);
                return;
            } //advances time
        }

        state.currentDayExerciseQuantity += 1;
        int calorieExpenditure = Mathf.RoundToInt(rnd.Next(360,505) * state.sleepQuality);
        state.calorieDifference -= calorieExpenditure;

        float energyDecreseValue = UnityEngine.Random.Range(-0.0003f, -0.0007f);
        energyDecreseValue *= calorieExpenditure;
        energy.ChangeEnergy(energyDecreseValue);

        float bgpChange = -1 * Mathf.Lerp(0, 0.75f * (calorieExpenditure * state.ptc), 1-state.diabetesSeverity);
        FindObjectOfType<BloodGlucose>().Change(bgpChange); 

        int hoursHE = Mathf.FloorToInt(hoursExercising);
        int minutesHE = Mathf.RoundToInt((hoursExercising - hoursHE) * 60);
        exerciseResultsText.text = hoursHE.ToString("00") + ":" + minutesHE.ToString("00") + "    " + calorieExpenditure + "kcal";

        resultsGO.SetActive(true);
        exerciseButton.SetActive(false);
        StartCoroutine(FindObjectOfType<ImageFade>().FadeImage());

        //?Debug.Log("Exercised for " + hoursExercising + " and spent " + calorieExpenditure + "kcal");
    }

    void WeightChange(){
        for(int i = 0; i<6; i++){
            state.exerciseHistory[i] = state.exerciseHistory[i + 1];
        }
        state.exerciseHistory[6] = state.currentDayExerciseQuantity;
        state.currentDayExerciseQuantity = 0;
        //?Debug.Log("Exercise history: " + state.exerciseHistory);

        float pastWeekExerciseSum =  state.exerciseHistory.Sum();

        state.activityFactor += (2 * Mathf.Cos(0.4f*pastWeekExerciseSum - Mathf.PI/2))/100f;
        state.activityFactor -= (-(1.4f*Mathf.Pow(state.activityFactor-2, 2))+2)/200f;
        state.activityFactor = Mathf.Clamp(state.activityFactor, 1, 2);
        //?Debug.Log("Activity Factor: " + state.activityFactor);

        float c = state.currentWeightKg - 
        ((state.calorieDifference - state.activityFactor * (6.25f * state.heightCentimeters - 5 * state.ageYears + state.sexFactor))/
        (10 * state.activityFactor));

        float weightChange = c * Mathf.Exp(-state.activityFactor/770) + ((state.calorieDifference - state.activityFactor * (6.25f * state.heightCentimeters - 5 * state.ageYears + state.sexFactor))/
        (10 * state.activityFactor));
        weightChange -= state.currentWeightKg;

        //?Debug.Log("Weight Change: " + weightChange);
        if(weightChange > 0){
            state.currentWeightKg += weightChange / state.sleepQuality;
        }else{
            state.currentWeightKg += weightChange * state.sleepQuality;
        }

        //?Debug.Log("New weight: " + state.currentWeightKg);

        state.bmi = state.currentWeightKg / Mathf.Pow(state.heightCentimeters / 100f, 2);

        state.basalCalorie = Mathf.RoundToInt(state.activityFactor * (10f * state.currentWeightKg + 
        6.25f * state.heightCentimeters - 5f * state.ageYears + state.sexFactor));
        state.dailyCalorieRealDefficit = state.calorieDifference - state.basalCalorie;
    }

    void Heart(){
        System.Random rnd = new System.Random();

        int goalHeartRate = 131 - 41 * (int)SaveManager.Instance.state.activityFactor;
        SaveManager.Instance.state.restingHeartRate = rnd.Next(goalHeartRate - 2, goalHeartRate + 3);
    }

}
