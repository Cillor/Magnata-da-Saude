using System;
using UnityEngine;
using TMPro;

namespace Timer{
    public class Clock : MonoBehaviour{
        public static bool timeStopped = false;

        private DateTime date = new DateTime(2021, 12, 31, 23, 59, 0);
        public DateTime Date{
            get{
                return date;
            } set{
                date = value;
                state.date = new int[]{date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second};
            }
        }

        private float clockSpeed;
        public float ClockSpeed{
            get{
                return clockSpeed;
            }
        }

        SaveState state = SaveManager.Instance.state;

        int clockIndex = -1;
        float[] speeds = {0.25f, 0.5f, 1f, 2f, 4f, 8f, 16f};


        private int currentDay, currentHour;


        public delegate void DayChangeAction();
        public static event DayChangeAction OnDayChange;
        
        public delegate void HourChangeAction();
        public static event HourChangeAction OnHourChange;

        private void Start() {
            date = new DateTime(state.date[0], state.date[1], state.date[2], state.date[3], state.date[4],state.date[5]);
            currentDay = date.Day;
            currentHour = date.Hour;

            GetComponent<Timer.UI.Clock>().UpdateGameClocks();

            ChangeClockSpeed();
        }

        private void Update() {
            FlowTime();

            Events();
        }

        public int AddTime(float hours){
            DateTime newDate = date.AddHours(hours);
            if(date.Hour <= 17 && newDate.Hour >= 13){
                Debug.Log("Cannot pass time, I need to go to school");
                return 0;
            }
            Date = date.AddHours(hours);

            for(int i = 0; i < Mathf.FloorToInt(hours); i++)
                OnHourChange();

            GetComponent<Timer.UI.Clock>().UpdateGameClocks();
            return 1;
        }

        public void SetHours(float hours){
            int hour = Mathf.FloorToInt(hours);
            int minute = Mathf.FloorToInt((hours-hour)*60);
            DateTime newDate = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            Date = newDate;
            GetComponent<Timer.UI.Clock>().UpdateGameClocks();
        }

        public void PauseClock(){
            timeStopped = !timeStopped;
            GetComponent<Timer.UI.Clock>().UpdateClockSpeed();
        }

        public void ChangeClockSpeed(){
            timeStopped = false;
            clockIndex++;
            clockIndex %= speeds.Length;
            clockSpeed = speeds[clockIndex];

            GetComponent<Timer.UI.Clock>().UpdateClockSpeed();
        }

        float timeCounter;
        private void FlowTime(){
            if(timeStopped || Pause.isPaused)
                return;

            timeCounter += Time.deltaTime;

            if(timeCounter > 1/clockSpeed){
                timeCounter = 0;
                AddTime(1/60f);
                GetComponent<Timer.UI.Clock>().UpdateGameClocks();
            }
        }

        private void Events(){
            if(currentDay != date.Day){
                currentDay = date.Day;
                if(OnDayChange != null)
                    OnDayChange();
            }

            if(currentHour != date.Hour){
                currentHour = date.Hour;
                if(OnHourChange != null)
                    OnHourChange();
            }
        }
    }
}