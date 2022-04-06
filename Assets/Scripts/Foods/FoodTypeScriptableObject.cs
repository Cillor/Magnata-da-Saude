using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "ScriptableObjects/FoodTypeScriptableObject", order = 1)]
public class FoodTypeScriptableObject : ScriptableObject
{
    public string food, weight; 
    public int calorieCost;
    public float carbs, protein, fat;
    public int consumingTime;
    public float energyExpenditure;

}
