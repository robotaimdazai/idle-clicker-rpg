
using UnityEngine;

[System.Serializable]
public class ManagerSaveData 
{
    public bool IsPurchased;

    public ManagerSaveData(bool isPurchased)
    {
        IsPurchased = isPurchased;
    }
}
