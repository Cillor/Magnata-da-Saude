using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BloodGlucose : MonoBehaviour{
    SaveState state;

    void Start(){
        state = SaveManager.Instance.state;

        Clock.OnDayChange += DailyUpdate;
        Clock.OnHourChange += PassiveBloodGlucoseReduction;
    }

    public void Change(float value){
        state.bgp += value;
        Debug.Log("BGP Changed");
        
        if(value < 0){
            FindObjectOfType<Indicators>().AddMessage("Glicose ↓", Color.red);
        }else{
            FindObjectOfType<Indicators>().AddMessage("Glicose ↑", Color.green);
        }
    }

    void DailyUpdate(){
        state.tdi = 0.6f * state.currentWeightKg;
        state.isf = 1500/state.tdi;
        state.icr = 500/state.tdi;
        state.insulinResistance = (((state.bmi / state.activityFactor)/state.bmi)-0.5f)*2f;
        state.ptc = Mathf.Lerp(0,state.icr/state.isf, state.insulinResistance);
        state.diabetesSeverity = state.bmi/50;
    }

    void PassiveBloodGlucoseReduction(){
        float f = (state.activityFactor/2f) * state.currentWeightKg;
        float bgpReduction = Mathf.Lerp(0, .75f * f * state.ptc, 1-state.diabetesSeverity);
        float bgpChange = -1 * (bgpReduction * (state.bgp/200f)); //TODO preciso arranjar um jeito de essa redução ser dependente do bgp;
        Change(bgpChange);     
    }
}
