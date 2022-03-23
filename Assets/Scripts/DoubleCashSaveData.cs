
using UnityEngine;

[System.Serializable]
public class DoubleCashSaveData
{
    public bool IsActive;
    public float EarnedTimeInSeconds;

    public DoubleCashSaveData() { }

    public DoubleCashSaveData(bool isActive, float earnedTimeInSeconds)
    {
        IsActive = isActive;
        EarnedTimeInSeconds = earnedTimeInSeconds;
    }
}
