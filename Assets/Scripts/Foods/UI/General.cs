using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Foods.UI{
    public class General : MonoBehaviour{
        public void DisplayFoodInOptions(FoodTypeScriptableObject food, string template, Transform location){
            GameObject newFoodType = Instantiate(Resources.Load(template, typeof(GameObject)), location) as GameObject;

            newFoodType.GetComponent<Foods.UI.FoodSelectionButton>().foodValues = food;

            TMP_Text name = newFoodType.transform.Find("Food").GetComponent<TMP_Text>();
            Image[] chart = newFoodType.transform.Find("macros").GetComponentsInChildren<Image>();
            System.Array.Reverse(chart);
            Image wholeness = newFoodType.transform.Find("Wholeness").GetComponent<Image>();
            TMP_Text calorieCost = newFoodType.transform.Find("calorieCost").GetComponent<TMP_Text>();
            
            float[] macros = new float[3]{food.protein, food.fat, food.carbs};
            name.text = food.food;
            SetValues(macros, chart);
            wholeness.fillAmount = food.processingLevel;
            calorieCost.text = food.calorieCost.ToString();

            food.go = newFoodType;
        }

        public void DisplayFoodInPlate(FoodTypeScriptableObject food, string template, Transform location){
            GameObject newFoodType = Instantiate(Resources.Load(template, typeof(GameObject)), location) as GameObject;

            newFoodType.GetComponent<Foods.UI.FoodSelectionButton>().foodValues = food;

            TMP_Text name = newFoodType.transform.Find("Food").GetComponent<TMP_Text>();
            TMP_Text weight = newFoodType.transform.Find("Weight").GetComponent<TMP_Text>();
            
            name.text = food.food;
            weight.text = food.weight + food.measure;
            food.go = newFoodType;
        }

        public void SetValues(float[] valuesToSet, Image[] imagesChart){
            float totalValues = 0;
            for(int i = 0; i < imagesChart.Length; i++){
                totalValues += FindPercentage(valuesToSet, i);
                imagesChart[i].fillAmount = totalValues;
            }
        }

        private float FindPercentage(float[] valueToSet, int index){
            float totalAmount = 0;
            for(int i = 0; i< valueToSet.Length; i++)
                totalAmount += valueToSet[i];
            
            return valueToSet[index] / totalAmount;
        }
    }
}