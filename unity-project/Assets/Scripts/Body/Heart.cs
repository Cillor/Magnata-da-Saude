using UnityEngine;

namespace Body{
    public class Heart : MonoBehaviour{
        SaveState state;

        private void Start() {
            state = SaveManager.Instance.state;

            Timer.Clock.OnDayChange += CalculateRestHeartRate;
        }

        void CalculateRestHeartRate(){
            System.Random rnd = new System.Random();

            int goalHeartRate = 131 - 41 * (int)SaveManager.Instance.state.activityFactor;
            SaveManager.Instance.state.restingHeartRate = rnd.Next(goalHeartRate - 2, goalHeartRate + 3);
            GetComponent<Body.UI.Heart>().UpdateUI();
        }
    }
}