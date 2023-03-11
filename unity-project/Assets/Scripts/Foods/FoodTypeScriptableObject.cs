using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "ScriptableObjects/FoodTypeScriptableObject", order = 1)]
public class FoodTypeScriptableObject : ScriptableObject{
    public string food, measure; 
    public float weight;
    public float calorieCost, carbs, protein, fat;
    public float consumingTime;
    public float fibers, water;
    public GameObject go;

}
