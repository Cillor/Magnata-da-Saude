using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Body.UI{
    public class BloodGlucose : MonoBehaviour {
        SaveState state;
        public TMP_Text bgpText;

        private void Start() {
            state = SaveManager.Instance.state;
        }

        public void UpdateUI(){
            bgpText.text = "Glicemia: " + state.bgp + "mg/dL";
        }
    }

}