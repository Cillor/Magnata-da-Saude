using System.Collections.Generic;
using UnityEngine;

namespace Foods{
    public class Plate : MonoBehaviour{
        public List<FoodTypeScriptableObject> foodPlate;

        public void EatPlate(){
            int calories = 0;
            float carbs = 0, protein = 0, fat = 0, processingAverage = 0;

            foreach(FoodTypeScriptableObject item in foodPlate){
                calories += item.calorieCost;
                carbs += item.carbs;
                protein += item.protein;
                fat += item.fat;
                processingAverage += item.processingLevel;
            }
            //Debug.Log(calories);
            processingAverage /= foodPlate.Count;
            GetComponent<Foods.UI.Plate>().RemoveFood();

            SaveManager.Instance.state.calorieDifference += calories;
            SaveManager.Instance.state.carbs += carbs;
            SaveManager.Instance.state.protein += protein;
            SaveManager.Instance.state.fat += fat;
            GetComponent<Foods.UI.General>().UpdateNutritionalInfo();

            FindObjectOfType<Energy>().ChangeEnergy(-1 * processingAverage/10f);
            SaveManager.Instance.state.sleepQuality -= processingAverage/10f;

            float kcalCarbs = carbs * 4;
            //Debug.Log("kcalCarbs: " + kcalCarbs);
            float carbToBloodStream = Mathf.Lerp(0, kcalCarbs, SaveManager.Instance.state.diabetesSeverity);
            //Debug.Log("CarbsToBS: " + carbToBloodStream);
            float bgpChange = carbToBloodStream * SaveManager.Instance.state.ptc;
            //Debug.Log("BGPChange: " + bgpChange);
            FindObjectOfType<BloodGlucose>().Change(bgpChange); 
        }
    }
}