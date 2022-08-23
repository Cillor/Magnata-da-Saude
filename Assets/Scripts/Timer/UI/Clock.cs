using UnityEngine;
using TMPro;

namespace Timer.UI{
    public class Clock : MonoBehaviour {
        [SerializeField] private TMP_Text mainClockText, bedClockText, gymClockText, computerClockText, clockSpeedText;

        public void UpdateGameClocks(){
            Timer.Clock clock = GetComponent<Timer.Clock>();
            mainClockText.text = clock.Date.ToString("dd/MM/yyyy HH:mm");
            bedClockText.text = clock.Date.ToString("HH:mm");
            gymClockText.text = clock.Date.ToString("HH:mm");
            computerClockText.text = clock.Date.ToString("HH:mm");
        }

        public void UpdateClockSpeed(){
            Timer.Clock clock = GetComponent<Timer.Clock>();

            if(Timer.Clock.timeStopped){
                clockSpeedText.text = "||";
                return;
            }

            if(clock.ClockSpeed <= 1)
                clockSpeedText.text = "► " + clock.ClockSpeed.ToString("0.00") + "x";
            else
                clockSpeedText.text = "►► " + clock.ClockSpeed.ToString("0") + "x";
        }

    }
}