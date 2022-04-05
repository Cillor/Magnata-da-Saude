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
        //?Debug.Log("Energy expent: " + change);
        float energyDelta;
        if(change >= 0){
            energyDelta = change * (68 - state.hoursSinceLastSlept)/52;
            energyDelta *= (2100 + state.dailyCalorieRealDefficit)/2100;
        }else{
            energyDelta = change * (88 + state.hoursSinceLastSlept)/104;
            energyDelta *= (2100 - state.dailyCalorieRealDefficit)/2100;
        }
        Debug.Log("Energy delta: " + energyDelta);

        state.energy += energyDelta;
        state.energy = Mathf.Clamp(state.energy, 0, 1);
        energySlider.value = state.energy;

        if(energyDelta < 0){
            FindObjectOfType<Indicators>().AddMessage("Energia ↓", Color.red);
        }else{
            FindObjectOfType<Indicators>().AddMessage("Energia ↑", Color.green);
        }

        if(state.energy <= 0){
            SceneManager.LoadScene("EnergyDeath");
        }

        //change graph
    }
}
