using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;

public class Exercise : MonoBehaviour{
    public TMP_Text exerciseResultsText;

    public GameObject resultsGO, exerciseButton;

    Energy energy;

    SaveState state = SaveManager.Instance.state;


    private void Start() {
        state = SaveManager.Instance.state;
        energy = FindObjectOfType<Energy>();
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
            if(FindObjectOfType<Timer.Clock>().AddTime(hoursExercising) == 0){
                FindObjectOfType<Indicators>().AddMessage("Sem tempo para se exercitar", Color.red);
                return;
            } //advances time
        }

        state.currentDayExerciseQuantity += 1;
        int calorieExpenditure = Mathf.RoundToInt(rnd.Next(360,505) * state.sleepQuality);
        state.calorieDifference -= calorieExpenditure;
        FindObjectOfType<Foods.UI.Fridge>().DailyNutritionalInfo();

        float energyDecreseValue = UnityEngine.Random.Range(-0.0003f, -0.0007f);
        energyDecreseValue *= calorieExpenditure;
        energy.ChangeEnergy(energyDecreseValue);

        float bgpChange = -1 * Mathf.Lerp(0, 0.75f * (calorieExpenditure * state.ptc), 1-state.diabetesSeverity);
        FindObjectOfType<Body.BloodGlucose>().Change(bgpChange); 

        int hoursHE = Mathf.FloorToInt(hoursExercising);
        int minutesHE = Mathf.RoundToInt((hoursExercising - hoursHE) * 60);
        exerciseResultsText.text = hoursHE.ToString("00") + ":" + minutesHE.ToString("00") + "    " + calorieExpenditure + "kcal";

        resultsGO.SetActive(true);
        exerciseButton.SetActive(false);
        StartCoroutine(FindObjectOfType<ImageFade>().FadeImage());

        //?Debug.Log("Exercised for " + hoursExercising + " and spent " + calorieExpenditure + "kcal");
    }

}
