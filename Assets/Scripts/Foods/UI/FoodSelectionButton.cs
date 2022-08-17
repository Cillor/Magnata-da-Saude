using UnityEngine;

namespace Foods.UI{
    public class FoodSelectionButton : MonoBehaviour{
        public FoodTypeScriptableObject foodValues;

        private void Start() {
            Foods.UI.Plate.OnCleanPlate += RemoveGameObject;
        }

        public void SelectFood(){
            Foods.UI.Plate plateUI = FindObjectOfType<Foods.UI.Plate>();
            plateUI.selectedFood = foodValues;
            plateUI.foodName.text = foodValues.food;
            plateUI.weightSlider.value = foodValues.weight;
            plateUI.weightValue.text = foodValues.weight + foodValues.measure;

            Foods.UI.General generalUI = new Foods.UI.General();
            float[] macros = new float[3]{foodValues.protein, foodValues.fat, foodValues.carbs};
            generalUI.SetValues(macros, plateUI.macroBarChart);
        }

        public void RemoveFoodFromPlate(){
            FindObjectOfType<Foods.Plate>().foodPlate.Remove(foodValues);
            FindObjectOfType<Foods.UI.Plate>().UpdateNutritionalInfo();
            RemoveGameObject();
        }

        void RemoveGameObject(){
            GameObject.Destroy(this.gameObject);
        }
    }
}