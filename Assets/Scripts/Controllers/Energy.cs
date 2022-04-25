using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Energy : MonoBehaviour{
    public Slider energySlider;
    SaveState state;

    private void Start() {
        state = SaveManager.Instance.state;
        energySlider.value = state.energy;

        Clock.OnHourChange += ExistenceEnergyExpenditure;
    }

    void ExistenceEnergyExpenditure(){
        ChangeEnergy(-0.001f);
    }

    public void ChangeEnergy(float change){
        Debug.Log("Energy expent: " + change);
        float energyDelta;
        if(change >= 0){
            energyDelta = change * (68f - state.hoursSinceLastSlept)/52f;
            Debug.Log("Calorie deficit mod: " + state.dailyCalorieRealDefficit);
            energyDelta *= (2100f + state.dailyCalorieRealDefficit)/2100f;
        }else{
            energyDelta = change * (88f + state.hoursSinceLastSlept)/104f;
            Debug.Log("Calorie deficit mod: " + state.dailyCalorieRealDefficit);
            energyDelta *= (2100f - state.dailyCalorieRealDefficit)/2100f;
        }


        state.energy += energyDelta;
        Debug.Log("EnergyDelta: "+ energyDelta);
        state.energy = Mathf.Clamp(state.energy, 0, 1);
        energySlider.value = state.energy;

        if(energyDelta < 0){
            FindObjectOfType<Indicators>().AddMessage("Energia ↓", Color.red);
        }else{
            FindObjectOfType<Indicators>().AddMessage("Energia ↑", Color.green);
        }
        //change graph
    }
}
