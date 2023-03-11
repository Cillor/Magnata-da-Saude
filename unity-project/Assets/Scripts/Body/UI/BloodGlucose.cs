using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Body.UI{
    public class BloodGlucose : MonoBehaviour {
        SaveState state;
        public TMP_Text bgpText;

        private void Start() {
            state = SaveManager.Instance.state;
            UpdateUI();
        }

        public void UpdateUI(){
            Debug.Log(state.bgp);
            bgpText.text = "Glicemia: " + state.bgp.ToString("F1") + "mg/dL";
        }
    }

}