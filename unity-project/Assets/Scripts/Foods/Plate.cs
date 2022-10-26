using System.Collections.Generic;
using UnityEngine;

namespace Foods{
    public class Plate : MonoBehaviour{
        private List<FoodTypeScriptableObject> foodPlate = new List<FoodTypeScriptableObject>();
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
            float calories = 0, carbs = 0, protein = 0, fat = 0, fiberCount = 0;

            foreach(FoodTypeScriptableObject item in foodPlate){
                calories += item.calorieCost;
                carbs += item.carbs;
                protein += item.protein;
                fat += item.fat;
                fiberCount += item.processingLevel;
            }
            //Debug.Log(calories);
            GetComponent<Foods.UI.Plate>().RemoveFood();

            SaveManager.Instance.state.calorieDifference += calories;
            SaveManager.Instance.state.carbs += carbs;
            SaveManager.Instance.state.protein += protein;
            SaveManager.Instance.state.fat += fat;
            GetComponent<Foods.UI.Fridge>().DailyNutritionalInfo();

            FindObjectOfType<Energy>().ChangeEnergy(-1 * (calories/100f)/fiberCount);
            SaveManager.Instance.state.sleepQuality -= (calories/100f)/fiberCount;

            FindObjectOfType<Body.BloodGlucose>().AddCarbsToBloodStream(4); 
        }
    }
}