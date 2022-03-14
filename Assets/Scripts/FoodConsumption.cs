using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodConsumption : MonoBehaviour{
    
    public FoodTypeScriptableObject foodValues;
    

    public void ConsumeFood(){
        SaveManager.Instance.state.calorieDifference += foodValues.calorieCost;
        SaveManager.Instance.state.carbs += foodValues.carbs;
        SaveManager.Instance.state.protein += foodValues.protein;
        SaveManager.Instance.state.fat += foodValues.fat;

        Debug.Log("Calories: " + SaveManager.Instance.state.calorieDifference);
    }
}
