using System;
using System.Linq;
using UnityEngine;
using TMPro;

public class Computer : MonoBehaviour
{
    public TMP_Text timeStudyingResultText;
    public GameObject resultsGO, studyButton;

    Energy energy;

    SaveState state = SaveManager.Instance.state;


    private void Start() {
        energy = FindObjectOfType<Energy>();
    }

    public void StudyAction(){
        System.Random rnd = new System.Random();

        float hoursInTheComputer = (float)rnd.NextDouble() * (1.5f - 0.5f) + 0.5f;

        if(SaveManager.Instance.state.tutorialCompleted){
            if(FindObjectOfType<Timer.Clock>().AddTime(hoursInTheComputer) == 0){
                Debug.Log("Cannot Study");
                return;
            } //advances time
        }
        int hoursHITC = Mathf.FloorToInt(hoursInTheComputer);
        int minutesHITC = Mathf.RoundToInt((hoursInTheComputer - hoursHITC) * 60);
        timeStudyingResultText.text = hoursHITC.ToString("00") + ":" + minutesHITC.ToString("00");

        //randomly gives or removes player energy
        float energyChangeValue = UnityEngine.Random.Range(-0.05f, 0.05f);
        energy.ChangeEnergy(energyChangeValue);

        resultsGO.SetActive(true);
        studyButton.SetActive(false);
        StartCoroutine(FindObjectOfType<ImageFade>().FadeImage());

    }
}
