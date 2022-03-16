using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodConsumption : MonoBehaviour{
    
    public FoodTypeScriptableObject foodValues;

    Clock clock;

    private void Start() {
        clock = GameObject.FindWithTag("clock").GetComponent<Clock>();
    }
    
    public void ConsumeFood(){
        Debug.Log(foodValues.consumingTime/60f);
        if(clock.AddTime(foodValues.consumingTime/60f) == 0){
            Debug.Log("Cannot eat");
            return;
        }
        SaveManager.Instance.state.calorieDifference += foodValues.calorieCost;
        SaveManager.Instance.state.carbs += foodValues.carbs;
        SaveManager.Instance.state.protein += foodValues.protein;
        SaveManager.Instance.state.fat += foodValues.fat;

    }
}
