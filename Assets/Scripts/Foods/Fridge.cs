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
                _f.weight = int.Parse(data[1]);
                _f.measure = data[2];
                int cals = int.Parse(data[3]);
                int carbs = int.Parse(data[4]);
                int fats = int.Parse(data[5]);
                int proteins = int.Parse(data[6]);
                _f.consumingTime = int.Parse(data[7]);
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