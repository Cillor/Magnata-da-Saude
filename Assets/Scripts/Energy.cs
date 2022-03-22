using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour{
    public Slider energySlider;
    SaveState state;

    private void Start() {
        state = SaveManager.Instance.state;
        energySlider.value = state.energy;
    }

    public void ChangeEnergy(float change){
        //?Debug.Log("Energy expent: " + change);
        state.energy += change;
        state.energy = Mathf.Clamp(state.energy, 0, 1);
        energySlider.value = state.energy;
        //change graph
    }
}
