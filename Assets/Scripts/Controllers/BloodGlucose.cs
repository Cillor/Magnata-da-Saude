using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodGlucose : MonoBehaviour{
    Clock clock;
    Energy energy;
    SaveState state;

    void Start(){
        clock = GameObject.FindWithTag("clock").GetComponent<Clock>();
        energy = GameObject.FindWithTag("energy").GetComponent<Energy>();
        state = SaveManager.Instance.state;

        Clock.OnDayChange += Change;
        Clock.OnHourChange += PassiveBloodGlucoseReduction;
    }

    void Change(){
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
        state.bgp -= bgpReduction * (state.bgp/200f); //TODO preciso arranjar um jeito de essa redução ser dependente do bgp;
    }
}
