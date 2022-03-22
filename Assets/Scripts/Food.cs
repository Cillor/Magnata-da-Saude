using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Food : MonoBehaviour{

    public FoodTypeScriptableObject[] foodList;
    public GameObject foodContainer;
    public GameObject foodTemplate;

    // Start is called before the first frame update
    void Start(){
        foreach (FoodTypeScriptableObject item in foodList){
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
