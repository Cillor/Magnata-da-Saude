using System.Collections.Generic;
using UnityEngine;

namespace Foods{
    public class Plate : MonoBehaviour{
        private List<FoodTypeScriptableObject> foodPlate;
        public List<FoodTypeScriptableObject> FoodPlate{
            get {return foodPlate;}
        }

        public void AddToPlate(FoodTypeScriptableObject food){
            foodPlate.Add(food);
        }
        public void RemoveFromPlate(FoodTypeScriptableObject food){
            foodPlate.Remove(food);
        }

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
            GetComponent<Foods.UI.Fridge>().DailyNutritionalInfo();

            FindObjectOfType<Energy>().ChangeEnergy(-1 * processingAverage/10f);
            SaveManager.Instance.state.sleepQuality -= processingAverage/10f;

            FindObjectOfType<BloodGlucose>().AddCarbsToBloodStream(4); 
        }
    }
}