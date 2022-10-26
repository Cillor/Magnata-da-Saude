using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "ScriptableObjects/FoodTypeScriptableObject", order = 1)]
public class FoodTypeScriptableObject : ScriptableObject{
    public string food, measure; 
    public int weight;
    public float calorieCost, carbs, protein, fat;
    public int consumingTime;
    public float processingLevel;
    public GameObject go;

}
