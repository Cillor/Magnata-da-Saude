using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

namespace Foods{
        public class Fridge : MonoBehaviour{
        
        [SerializeField]
        private GameObject foodTable;
        private List<FoodTypeScriptableObject> foodOptionsList;

        private void Start() {
            foodOptionsList = LoadFoodDatabase(Resources.Load<TextAsset>("FoodDB"));
            CreateTable(foodOptionsList);
        }

        List<FoodTypeScriptableObject> LoadFoodDatabase(TextAsset dataset){
            List<FoodTypeScriptableObject> foodList = new List<FoodTypeScriptableObject>();

            //splits the csv
            string[] lines = dataset.text.Split('\n');

            for(int i = 1; i < lines.Length - 1; i++) {
            var data = lines[i].Split(';');

            FoodTypeScriptableObject _f = new FoodTypeScriptableObject();
                //get raw data
                _f.food = data[0];
                _f.weight = int.Parse(data[1], CultureInfo.InvariantCulture);
                _f.measure = data[2];
                float cals = float.Parse(data[3], CultureInfo.InvariantCulture);
                float carbs = float.Parse(data[4], CultureInfo.InvariantCulture);
                Debug.Log(data[5]);
                float fats = float.Parse(data[5], CultureInfo.InvariantCulture);
                float proteins = float.Parse(data[6], CultureInfo.InvariantCulture);
                _f.consumingTime = int.Parse(data[7], CultureInfo.InvariantCulture);
                _f.processingLevel = float.Parse(data[8], CultureInfo.InvariantCulture);

                //data processing
                _f.calorieCost = (100 * cals)/_f.weight;
                _f.carbs = (100 * carbs)/_f.weight;
                _f.fat = (100 * fats)/_f.weight;
                _f.protein = (100 * proteins)/_f.weight;


                foodList.Add(_f);
            }

            return foodList;
        }

        void CreateTable(List<FoodTypeScriptableObject> foodL){
            Foods.UI.Fridge fridgeUI = GetComponent<Foods.UI.Fridge>();
            foreach (FoodTypeScriptableObject item in foodL){
                fridgeUI.DisplayFoodInOptions(item, "foodOptionButton", foodTable.transform);
            }
        }
    }
}