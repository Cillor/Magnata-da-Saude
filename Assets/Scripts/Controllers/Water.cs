using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour{
    public Slider waterIndicator;
    SaveState state;
    void Start(){
        state = SaveManager.Instance.state;
        waterIndicator.value = state.hydration;

        Clock.OnHourChange += Dehydrate;
    }

    public void Hydrate(float value){
        state.hydration += value;
        state.hydration = Mathf.Clamp01(state.hydration);
        waterIndicator.value = state.hydration;
        FindObjectOfType<Indicators>().AddMessage("Hidratação ↑", Color.green);

    }

    void Dehydrate(){
        state.hydration -= 0.01f;
        waterIndicator.value = state.hydration;
        FindObjectOfType<Indicators>().AddMessage("Hidratação ↓", Color.red);
    }
}
