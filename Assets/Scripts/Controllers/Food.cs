using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            _f.weight = data[1];
            _f.calorieCost = int.Parse(data[2]);
            _f.carbs = int.Parse(data[3]);
            _f.fat = int.Parse(data[4]);
            _f.protein = int.Parse(data[5]);
            _f.consumingTime = int.Parse(data[6]);
            _f.processingLevel = float.Parse(data[7]);

            foodL.Add(_f);
        }

 

        foreach (FoodTypeScriptableObject item in foodL){
            GameObject newFoodType = Instantiate(foodTemplate, foodContainer.transform);

            newFoodType.GetComponent<FoodConsumption>().foodValues = item;
            TMP_Text food = newFoodType.transform.Find("Food").GetComponent<TMP_Text>();
            TMP_Text quantity = newFoodType.transform.Find("Quantity").GetComponent<TMP_Text>();
            TMP_Text calorieCost = newFoodType.transform.Find("calorieCost").GetComponent<TMP_Text>();
            TMP_Text carb = newFoodType.transform.Find("carb").GetComponent<TMP_Text>();
            TMP_Text fat = newFoodType.transform.Find("fat").GetComponent<TMP_Text>();
            TMP_Text protein = newFoodType.transform.Find("protein").GetComponent<TMP_Text>();
            TMP_Text consumingTime = newFoodType.transform.Find("consumingTime").GetComponent<TMP_Text>();

            food.text = item.food;
            quantity.text = item.weight.ToString();
            calorieCost.text = item.calorieCost.ToString();
            carb.text = item.carbs.ToString();
            fat.text = item.fat.ToString();
            protein.text = item.protein.ToString();
            consumingTime.text = item.consumingTime.ToString();

        }
    }
}
