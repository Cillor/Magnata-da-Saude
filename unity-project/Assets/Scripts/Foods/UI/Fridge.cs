using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Foods.UI{
    public class Fridge : MonoBehaviour{
        [SerializeField]
        private Image[] pieChartImage;
        [SerializeField]
        private TMP_Text[] pieChartText;
        [SerializeField]
        private TMP_Text caloriesText;

        private SaveState state;

        //UNITY METHODS
        private void Start() {
            state = SaveManager.Instance.state;
            DailyNutritionalInfo();
        }

        public void DailyNutritionalInfo(){
            caloriesText.text = state.calorieDifference + "kcal";
            float[] pieChartValues = new float[3]{state.protein, state.fat, state.carbs};
            SetChartValues(pieChartValues, pieChartImage);
            SetTextValues(pieChartValues, pieChartText);
        }

        //PUBLIC METHODS
        public void DisplayFoodInOptions(FoodTypeScriptableObject food, string template, Transform location){
            GameObject newFoodType = Instantiate(Resources.Load(template, typeof(GameObject)), location) as GameObject;

            newFoodType.GetComponent<Foods.UI.FoodSelectionButton>().FoodValues = food;

            TMP_Text name = newFoodType.transform.Find("Food").GetComponent<TMP_Text>();
            Image[] chart = newFoodType.transform.Find("macros").GetComponentsInChildren<Image>();
            System.Array.Reverse(chart);
            //Image wholeness = newFoodType.transform.Find("Wholeness").GetComponent<Image>();
            TMP_Text calorieCost = newFoodType.transform.Find("calorieCost").GetComponent<TMP_Text>();
            
            float[] macros = new float[3]{food.protein, food.fat, food.carbs};
            name.text = food.food;
            SetChartValues(macros, chart);
            //wholeness.fillAmount = food.processingLevel;
            calorieCost.text = food.calorieCost.ToString("F1");

            food.go = newFoodType;
        }

        public void SetChartValues(float[] valuesToSet, Image[] imagesChart){
            float totalValues = 0;
            for(int i = 0; i < imagesChart.Length; i++){
                totalValues += FindPercentage(valuesToSet, i);
                imagesChart[i].fillAmount = totalValues;
            }
        }
        
        //PRIVATE METHODS
        private void SetTextValues(float[] valuesToSet, TMP_Text[] chartText){
            for(int i = 0; i < chartText.Length; i++){
                chartText[i].text = valuesToSet[i].ToString("(0g)") + " - " + FindPercentage(valuesToSet, i).ToString("0%");
            }
        }

        private float FindPercentage(float[] valueToSet, int index){
            float totalAmount = 0;
            for(int i = 0; i< valueToSet.Length; i++)
                totalAmount += valueToSet[i];
            
            float ret = valueToSet[index] / totalAmount;

            return totalAmount != 0 ? ret : 0;
        }
    }
}