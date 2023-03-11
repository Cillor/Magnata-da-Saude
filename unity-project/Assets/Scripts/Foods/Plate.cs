using System.Collections.Generic;
using UnityEngine;

namespace Foods{
    public class Plate : MonoBehaviour{
        private List<FoodTypeScriptableObject> foodPlate = new List<FoodTypeScriptableObject>();
        public List<FoodTypeScriptableObject> FoodPlate{
            get {return foodPlate;}
        }

        public void AddToPlate(FoodTypeScriptableObject food){
            if(foodPlate.Contains(food)){
                int i = foodPlate.FindIndex(p => p.food == food.food);
                foodPlate[i].weight += food.weight;

                foodPlate[i].calorieCost += food.calorieCost;
                foodPlate[i].carbs += food.carbs;
                foodPlate[i].fat += food.fat;
                foodPlate[i].protein += food.protein;
            }else{
                foodPlate.Add(food);
            }
        }

        public void RemoveFromPlate(FoodTypeScriptableObject food){
            foodPlate.Remove(food);
        }

        public void EatPlate(){
            float calories = 0, carbs = 0, protein = 0, fat = 0, fibers = 0, water = 0, time = 0;

            foreach(FoodTypeScriptableObject item in foodPlate){
                calories += item.calorieCost;
                carbs += item.carbs;
                protein += item.protein;
                fat += item.fat;
                fibers += item.fibers;
                water += item.water;
                time += item.consumingTime;
            }
            //Debug.Log(calories);
            GetComponent<Foods.UI.Plate>().RemoveFood();

            SaveManager.Instance.state.calorieDifference += calories;
            SaveManager.Instance.state.carbs += carbs;
            SaveManager.Instance.state.protein += protein;
            SaveManager.Instance.state.fat += fat;
            GetComponent<Foods.UI.Fridge>().DailyNutritionalInfo();

            FindObjectOfType<Energy>().ChangeEnergy((fibers + (water/1000))/100);
            //!there should be a way in here that the type of food you each impacts your sleep
            //!SaveManager.Instance.state.sleepQuality -= ;

            FindObjectOfType<Body.BloodGlucose>().AddCarbsToBloodStream(carbs); 

            FindObjectOfType<Timer.Clock>().AddTime(time/60f);
        }
    }
}