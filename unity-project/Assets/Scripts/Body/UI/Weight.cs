using UnityEngine;
using TMPro;

namespace Body.UI{
    public class Weight : MonoBehaviour{
        SaveState state;
        public TMP_Text weightText, bmiText;

        private void Start() {
            state = SaveManager.Instance.state;
            UpdateUI();
        }

        public void UpdateUI(){
            weightText.text = "Peso: " + state.currentWeightKg.ToString("F1") + "kg";
            bmiText.text = "IMC: " + state.bmi.ToString("F2");
        }
    }
}