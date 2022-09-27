using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Body.UI{
    public class Heart : MonoBehaviour {
        SaveState state;
        public TMP_Text restingHeartRateText;

        private void Start() {
            state = SaveManager.Instance.state;
        }

        public void UpdateUI(){
            restingHeartRateText.text = "FCD: " + state.restingHeartRate;
        }
    }

}