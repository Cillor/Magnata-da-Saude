using System;

public class SaveState
{
    public float energy = 1;

    public int ageYears = 16, heightCentimeters = 172, calorieDifference = 0, currentDayExerciseQuantity = 0;
    
    //sexFactor +5 for men and -161 for women
    public float activityFactor = 1f, sexFactor = 5f; 
    public float currentWeightKg = 110, bmi = 37.1f; 

    //49-55 / 56-61 / 62-65 / 66-69 / 70-73 / 74-81 / 82+
    public int restingHeartRate = 80;

    //changing this will change the game difficulty
    public float totalHoursSlept = 350, numberOfSleeps = 50, sleepQuality = 0.84f;
    
    public float carbs, fat, protein;

    public int[] date = new int[6]{2022, 1, 1, 0, 0, 0};
    public int[] exerciseHistory = new int[7]{0, 0, 0, 0, 0, 0, 0};


    public float bgp = 120, tdi, isf, icr, ptc, insulinResistance, diabetesSeverity;
}
