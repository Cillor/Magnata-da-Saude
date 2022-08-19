using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Foods.UI{
    public class General : MonoBehaviour{
        SaveState state;

        public Image[] pieChartImage;
        public TMP_Text[] pieChartText;
        public TMP_Text caloriesText;

        private void Start() {
            state = SaveManager.Instance.state;
            UpdateNutritionalInfo();
        }

        public void UpdateNutritionalInfo(){
            caloriesText.text = state.calorieDifference + "kcal";
            float[] pieChartValues = new float[3]{state.protein, state.fat, state.carbs};
            SetChartValues(pieChartValues, pieChartImage);
            SetTextValues(pieChartValues, pieChartText);
        }

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
            SetChartValues(macros, chart);
            wholeness.fillAmount = food.processingLevel;
            calorieCost.text = food.calorieCost.ToString();

            food.go = newFoodType;
        }

        public void SetChartValues(float[] valuesToSet, Image[] imagesChart){
            float totalValues = 0;
            for(int i = 0; i < imagesChart.Length; i++){
                totalValues += FindPercentage(valuesToSet, i);
                imagesChart[i].fillAmount = totalValues;
            }
        }
        
        public void SetTextValues(float[] valuesToSet, TMP_Text[] chartText){
            for(int i = 0; i < chartText.Length; i++){
                chartText[i].text = valuesToSet[i].ToString("(0g)") + " - " + FindPercentage(valuesToSet, i).ToString("0%");
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