using System;

public class SaveState
{
    public int ageYears, heightCentimeters, calorieDifference, currentDayExerciseQuantity;
    public float currentWeightKg, activityFactor, sexFactor, sleepQuality;

    public int[] date = new int[6]{2022, 1, 1, 0, 0, 0};
    public int[] exerciseHistory = new int[7]{0, 0, 0, 0, 0, 0, 0};
}
