using UnityEngine;
using UnityEngine.UI;

namespace Body{
    public class Water : MonoBehaviour{
        public Slider waterIndicator;
        SaveState state;
        void Start(){
            state = SaveManager.Instance.state;
            waterIndicator.value = state.hydration;

            Timer.Clock.OnHourChange += Dehydrate;
        }

        public void ChangeWater(float value){
            state.hydration += value;
            state.hydration = Mathf.Clamp01(state.hydration);
            waterIndicator.value = state.hydration;
            if(value > 0){
                FindObjectOfType<Indicators>().AddMessage("Hidratação ↑", Color.green);
            }else{
                FindObjectOfType<Indicators>().AddMessage("Hidratação ↓", Color.red);
            }

        }

        void Dehydrate(){
            ChangeWater(-0.01f);
        }
    }
}
