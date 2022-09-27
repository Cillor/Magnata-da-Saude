using UnityEngine;
using TMPro;

namespace Body.UI{
    public class Weight : MonoBehaviour{
        SaveState state;
        public TMP_Text weightText, bmiText;

        private void Start() {
            state = SaveManager.Instance.state;
        }

        public void UpdateUI(){
            weightText.text = "Peso: " + state.currentWeightKg.ToString("0.00") + "kg";
            bmiText.text = "IMC: " + state.bmi.ToString("0.00");
        }
    }
}