using UnityEngine;

namespace Foods.UI{
    public class FoodSelectionButton : MonoBehaviour{
        private FoodTypeScriptableObject foodValues;
        public FoodTypeScriptableObject FoodValues {
            get; set;
        }

        public void SelectFood(){
            FindObjectOfType<Foods.UI.Plate>().SelectFood(foodValues);
        }

        public void RemoveFoodFromPlate(){
            FindObjectOfType<Foods.Plate>().RemoveFromPlate(foodValues);
            FindObjectOfType<Foods.UI.Plate>().PlateNutritionalInfo();
            RemoveGameObject();
        }

        void RemoveGameObject(){
            GameObject.Destroy(this.gameObject);
        }
    }
}