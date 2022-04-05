using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodConsumption : MonoBehaviour{
    
    public FoodTypeScriptableObject foodValues;

    public Sprite sprite;

    Clock clock;
    Energy energy;

    private void Start() {
        clock = GameObject.FindWithTag("clock").GetComponent<Clock>();
        energy = GameObject.FindWithTag("energy").GetComponent<Energy>();
    }
    
    public void ConsumeFood(){
        if(clock.AddTime(foodValues.consumingTime/60f) == 0){
            FindObjectOfType<Indicators>().AddMessage("Sem tempo para comer", Color.red);
            return;
        }

        FindObjectOfType<ImageFade>().ImageFader(sprite);

        SaveManager.Instance.state.calorieDifference += foodValues.calorieCost;
        SaveManager.Instance.state.carbs += foodValues.carbs;
        SaveManager.Instance.state.protein += foodValues.protein;
        SaveManager.Instance.state.fat += foodValues.fat;
        energy.ChangeEnergy(foodValues.energyExpenditure);

        float kcalCarbs = foodValues.carbs * 4;
        float carbToBloodStream = Mathf.Lerp(0, kcalCarbs, SaveManager.Instance.state.diabetesSeverity);
        float bgpChange = carbToBloodStream * SaveManager.Instance.state.ptc;
        FindObjectOfType<BloodGlucose>().Change(bgpChange); 
    }
}
