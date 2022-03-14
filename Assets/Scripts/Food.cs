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
            Instantiate(foodTemplate, foodContainer.transform);
            foodTemplate.GetComponent<FoodConsumption>().foodValues = item;

            Debug.Log(item.food + " criado! " + foodTemplate.GetComponent<FoodConsumption>().foodValues);

            TMP_Text food = foodTemplate.transform.Find("Food").GetComponent<TMP_Text>();
            TMP_Text quantity = foodTemplate.transform.Find("Quantity").GetComponent<TMP_Text>();
            TMP_Text calorieCost = foodTemplate.transform.Find("calorieCost").GetComponent<TMP_Text>();
            TMP_Text carb = foodTemplate.transform.Find("carb").GetComponent<TMP_Text>();
            TMP_Text fat = foodTemplate.transform.Find("fat").GetComponent<TMP_Text>();
            TMP_Text protein = foodTemplate.transform.Find("protein").GetComponent<TMP_Text>();
            TMP_Text consumingTime = foodTemplate.transform.Find("consumingTime").GetComponent<TMP_Text>();

            food.text = item.food;
            quantity.text = item.weight.ToString();
            calorieCost.text = item.calorieCost.ToString();
            carb.text = item.carbs.ToString();
            fat.text = item.fat.ToString();
            protein.text = item.protein.ToString();
            consumingTime.text = item.consumingTime.ToString();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
