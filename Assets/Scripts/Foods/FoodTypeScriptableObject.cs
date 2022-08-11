using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "ScriptableObjects/FoodTypeScriptableObject", order = 1)]
public class FoodTypeScriptableObject : ScriptableObject{
    public string food, measure; 
    public int weight, calorieCost;
    public float carbs, protein, fat;
    public int consumingTime;
    public float processingLevel;

}
