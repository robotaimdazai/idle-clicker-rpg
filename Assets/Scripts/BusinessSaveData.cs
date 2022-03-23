
using UnityEngine;

[System.Serializable]
public class BusinessSaveData 
{
    public double Price;
    public double Earning;
    public double ExpandPrice;
    public int EarningMultiplier;
    public int Level;
    public float EarnTime;
    public bool IsPurchased = false;
    public bool HasManager;
    public bool IsAutomated;

    public BusinessSaveData() { }

    public BusinessSaveData(double price, double earning, double expandPrice, int earningMultiplier, int level, float earnTime, bool isPurchased, bool hasManager, bool isAutomated)
    {
        Price = price;
        Earning = earning;
        ExpandPrice = expandPrice;
        EarningMultiplier = earningMultiplier;
        Level = level;
        EarnTime = earnTime;
        IsPurchased = isPurchased;
        HasManager = hasManager;
        IsAutomated = isAutomated;
    }
}
