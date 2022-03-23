using UnityEngine;

[System.Serializable]
public class UpgradeSaveData
{
    public bool IsPurchased;

    public UpgradeSaveData() { }

    public UpgradeSaveData(bool isPurchased)
    {
        IsPurchased = isPurchased;
    }
}
