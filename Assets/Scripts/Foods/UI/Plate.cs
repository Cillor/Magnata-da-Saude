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

            GetComponent<Foods.Plate>().foodPlate.Add(selectedFood);

            Foods.UI.General generalUI = new Foods.UI.General();
            generalUI.DisplayFoodInPlate(selectedFood, "foodInPlateButton", plateContainer.transform);
            generalUI.SetValues(new float[3]{0, 0, 0}, macroBarChart);
            foodName.text = "-";

            selectedFood = null;
            UpdateNutritionalInfo();
        }

        public void ChangeCurrentFoodQuantity(float weight){
            selectedFood.weight = (int)weight;
            selectedFood.calorieCost = ((int)weight * selectedFood.calorieCost)/100;
            selectedFood.carbs = (weight * selectedFood.carbs)/100f;
            selectedFood.fat = (weight * selectedFood.fat)/100f;
            selectedFood.protein = (weight * selectedFood.protein)/100f;
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


        public delegate void CleanPlate();
        public static event CleanPlate OnCleanPlate;

        public void RemoveFood(){
            List<FoodTypeScriptableObject> plate = GetComponent<Foods.Plate>().foodPlate;

            foreach(FoodTypeScriptableObject item in plate){
                Destroy(item.go.gameObject);
            }
            plate.Clear();

            float[] zero = new float[3]{0, 0, 0};
            PizzaGraph(zero, 0);
        }

        void PizzaGraph(float[] macros, int calories){
            Foods.UI.General generalUI = new Foods.UI.General();
            generalUI.SetValues(macros, macroPizzaChart);
            totalCalories.text = calories + "kcal";
        }
    }
}