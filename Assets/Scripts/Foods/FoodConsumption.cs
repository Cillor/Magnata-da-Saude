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
        StartCoroutine(FindObjectOfType<ImageFade>().FadeImage());

        SaveManager.Instance.state.calorieDifference += foodValues.calorieCost;
        SaveManager.Instance.state.carbs += foodValues.carbs;
        SaveManager.Instance.state.protein += foodValues.protein;
        SaveManager.Instance.state.fat += foodValues.fat;
        //Debug.Log("Food eaten");
        energy.ChangeEnergy(-1 * foodValues.processingLevel/10f);
        SaveManager.Instance.state.sleepQuality -= foodValues.processingLevel/10f;

        float kcalCarbs = foodValues.carbs * 4;
        //Debug.Log("kcalCarbs: " + kcalCarbs);
        float carbToBloodStream = Mathf.Lerp(0, kcalCarbs, SaveManager.Instance.state.diabetesSeverity);
        //Debug.Log("CarbsToBS: " + carbToBloodStream);
        float bgpChange = carbToBloodStream * SaveManager.Instance.state.ptc;
        //Debug.Log("BGPChange: " + bgpChange);
        FindObjectOfType<BloodGlucose>().Change(bgpChange); 
    }
}
