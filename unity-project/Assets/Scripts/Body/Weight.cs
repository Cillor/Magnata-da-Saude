using System.Linq;
using UnityEngine;

namespace Body{
    public class Weight : MonoBehaviour{
        SaveState state;

        private void Start() {
            state = SaveManager.Instance.state;
            Timer.Clock.OnDayChange += WeightChange;
        }

        void CalculateExerciseMovingAverage(){
            for(int i = 0; i<6; i++){
                state.exerciseHistory[i] = state.exerciseHistory[i + 1];
            }
            state.exerciseHistory[6] = state.currentDayExerciseQuantity;
            state.currentDayExerciseQuantity = 0;
            //?Debug.Log("Exercise history: " + state.exerciseHistory);
        }

        void CalculateActivityFactor(){
            float pastWeekExerciseSum =  state.exerciseHistory.Sum();

            state.activityFactor += (2 * Mathf.Cos(0.4f*pastWeekExerciseSum - Mathf.PI/2))/100f;
            state.activityFactor -= (-(1.4f*Mathf.Pow(state.activityFactor-2, 2))+2)/200f;
            state.activityFactor = Mathf.Clamp(state.activityFactor, 1, 2);
            //?Debug.Log("Activity Factor: " + state.activityFactor);
        }

        void WeightChange(){
            CalculateExerciseMovingAverage();
            CalculateActivityFactor();

            float c = state.currentWeightKg - 
            ((state.calorieDifference - state.activityFactor * (6.25f * state.heightCentimeters - 5 * state.ageYears + state.sexFactor))/
            (10 * state.activityFactor));

            float weightChange = c * Mathf.Exp(-state.activityFactor/770) + ((state.calorieDifference - state.activityFactor * (6.25f * state.heightCentimeters - 5 * state.ageYears + state.sexFactor))/
            (10 * state.activityFactor));
            weightChange -= state.currentWeightKg;

            if(weightChange > 0){
                state.currentWeightKg += weightChange / state.sleepQuality; //sleep quality [0;1] will increase weight change if the person gains weight
            }else{
                state.currentWeightKg += weightChange * state.sleepQuality; //sleep quality [0;1] will decrease weight loss
            }
            //?Debug.Log("New weight: " + state.currentWeightKg);

            state.bmi = state.currentWeightKg / Mathf.Pow(state.heightCentimeters / 100f, 2);
            GetComponent<Body.UI.Weight>().UpdateUI();

            state.basalCalorie = Mathf.RoundToInt(state.activityFactor * (10f * state.currentWeightKg + 
            6.25f * state.heightCentimeters - 5f * state.ageYears + state.sexFactor));
            state.dailyCalorieRealDefficit = state.calorieDifference - state.basalCalorie;
        }

    }
}