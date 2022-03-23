
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public double Coins;
    public int SuperCash;

    public PlayerSaveData() { }

    public PlayerSaveData(double coins, int superCash)
    {
        Coins = coins;
        SuperCash = superCash;
    }
}
