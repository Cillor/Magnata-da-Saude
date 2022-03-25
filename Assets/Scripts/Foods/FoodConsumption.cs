using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodConsumption : MonoBehaviour{
    
    public FoodTypeScriptableObject foodValues;

    Clock clock;
    Energy energy;

    private void Start() {
        clock = GameObject.FindWithTag("clock").GetComponent<Clock>();
        energy = GameObject.FindWithTag("energy").GetComponent<Energy>();
    }
    
    public void ConsumeFood(){
        if(foodValues.energyExpenditure < 0 && SaveManager.Instance.state.energy < UnityEngine.Random.Range(0.3f, 0.5f)){
            Debug.Log("No energy to eat this type of food");
            return;
        }
        if(clock.AddTime(foodValues.consumingTime/60f) == 0){
            Debug.Log("Cannot eat");
            return;
        }
        SaveManager.Instance.state.calorieDifference += foodValues.calorieCost;
        SaveManager.Instance.state.carbs += foodValues.carbs;
        SaveManager.Instance.state.protein += foodValues.protein;
        SaveManager.Instance.state.fat += foodValues.fat;
        energy.ChangeEnergy(foodValues.energyExpenditure);

        float kcalCarbs = foodValues.carbs * 4;
        float carbToBloodStream = Mathf.Lerp(0, kcalCarbs, SaveManager.Instance.state.diabetesSeverity);
        SaveManager.Instance.state.bgp += carbToBloodStream * SaveManager.Instance.state.ptc;
    }
}
