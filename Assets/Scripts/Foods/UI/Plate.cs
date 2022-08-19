using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Foods.UI{
    public class Plate : MonoBehaviour{
        //[HideInInspector]
        public FoodTypeScriptableObject selectedFood;
        public GameObject plateContainer;

        [Header("Food Selection")]
        public TMP_Text foodName;
        public Slider weightSlider;
        public TMP_Text weightValue;
        public Image[] macroBarChart;

        [Header("Nutritional Info")]
        public Image[] macroPizzaChart;
        public TMP_Text totalCalories;

        public void AddCurrentFoodToPlate(){
            if(selectedFood == null)
                return;

            ChangeCurrentFoodQuantity(weightSlider.value);
            GetComponent<Foods.Plate>().foodPlate.Add(selectedFood);

            Foods.UI.General generalUI = GetComponent<Foods.UI.General>();
            DisplayFoodInPlate(selectedFood, "foodInPlateButton", plateContainer.transform);
            generalUI.SetChartValues(new float[3]{0, 0, 0}, macroBarChart);
            foodName.text = "-";
            //Debug.Log("Cal: " + selectedFood.calorieCost);

            selectedFood = null;
            UpdateNutritionalInfo();
        }

        void ChangeCurrentFoodQuantity(float weight){
            selectedFood.calorieCost = ((int)weight * selectedFood.calorieCost)/100;
            selectedFood.carbs = (weight * selectedFood.carbs)/100;
            selectedFood.fat = (weight * selectedFood.fat)/100;
            selectedFood.protein = (weight * selectedFood.protein)/100;
            selectedFood.weight = (int)weight;
        }

        public void UpdateFoodWeight(float weight){
            weightValue.text = weight + selectedFood.measure;
        }

        public void UpdateNutritionalInfo(){
            List<FoodTypeScriptableObject> plate = GetComponent<Foods.Plate>().foodPlate;

            int calories = 0;
            float carbs = 0, proteins = 0, fats = 0;
            foreach(FoodTypeScriptableObject food in plate){
                calories += food.calorieCost;
                carbs += food.carbs;
                proteins += food.protein;
                fats += food.fat;
            }
            float[] macros = new float[3]{proteins, fats, carbs};
            PizzaGraph(macros, calories);
        }

        void DisplayFoodInPlate(FoodTypeScriptableObject food, string template, Transform location){
            GameObject newFoodType = Instantiate(Resources.Load(template, typeof(GameObject)), location) as GameObject;

            newFoodType.GetComponent<Foods.UI.FoodSelectionButton>().foodValues = food;

            TMP_Text name = newFoodType.transform.Find("Food").GetComponent<TMP_Text>();
            TMP_Text weight = newFoodType.transform.Find("Weight").GetComponent<TMP_Text>();
            
            name.text = food.food;
            weight.text = food.weight + food.measure;
            food.go = newFoodType;
        }

        public void RemoveFood(){
            List<FoodTypeScriptableObject> plate = GetComponent<Foods.Plate>().foodPlate;

            foreach(FoodTypeScriptableObject item in plate){
                Destroy(item.go);
            }
            plate.Clear();

            float[] zero = new float[3]{0, 0, 0};
            PizzaGraph(zero, 0);
        }

        void PizzaGraph(float[] macros, int calories){
            Foods.UI.General generalUI = GetComponent<Foods.UI.General>();
            generalUI.SetChartValues(macros, macroPizzaChart);
            totalCalories.text = calories + "kcal";
        }
    }
}