using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Food : MonoBehaviour{

    public FoodTypeScriptableObject[] foodList;
    public GameObject foodContainer;
    public GameObject foodTemplate;

    List<FoodTypeScriptableObject> foodL = new List<FoodTypeScriptableObject>();

    // Start is called before the first frame update
    void Start(){
        var dataset = Resources.Load<TextAsset>("FoodDB");
        var lines = dataset.text.Split('\n');
        
        var lists = new List<List<string>>();
        for(int i = 1; i < lines.Length - 1; i++) {
            var data = lines[i].Split(';');

            FoodTypeScriptableObject _f = new FoodTypeScriptableObject();
            _f.food = data[0];
            _f.weight = int.Parse(data[1]);
            _f.measure = data[2];
            int cals = int.Parse(data[3]);
            int carbs = int.Parse(data[4]);
            int fats = int.Parse(data[5]);
            int proteins = int.Parse(data[6]);

            _f.calorieCost = (100 * cals)/_f.weight;
            _f.carbs = (100 * carbs)/_f.weight;
            _f.fat = (100 * fats)/_f.weight;
            _f.protein = (100 * proteins)/_f.weight;

            _f.consumingTime = int.Parse(data[7]);
            _f.processingLevel = float.Parse(data[8], CultureInfo.InvariantCulture);

            foodL.Add(_f);
        }

 

        foreach (FoodTypeScriptableObject item in foodL){
            GameObject newFoodType = Instantiate(foodTemplate, foodContainer.transform);

            newFoodType.GetComponent<FoodConsumption>().foodValues = item;
            TMP_Text food = newFoodType.transform.Find("Food").GetComponent<TMP_Text>();
            Image[] chart = newFoodType.transform.Find("macros").GetComponentsInChildren<Image>();
            System.Array.Reverse(chart);
            Image wholeness = newFoodType.transform.Find("Wholeness").GetComponent<Image>();
            TMP_Text calorieCost = newFoodType.transform.Find("calorieCost").GetComponent<TMP_Text>();
            float[] macros = new float[3]{item.protein, item.fat, item.carbs};

            food.text = item.food;
            SetValues(macros, chart);
            wholeness.fillAmount = item.processingLevel;
            calorieCost.text = item.calorieCost.ToString();
        }
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
        for(int i = 0; i< valueToSet.Length; i++){
        totalAmount += valueToSet[i];
        }
        return valueToSet[index] / totalAmount;
    }
}
