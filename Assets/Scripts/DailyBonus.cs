using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonus : MonoBehaviour
{
    public int Amount = 1;
    public BonusType Type;
    public enum BonusType { Coins, SuperCash, SkipTime }

    public void Claim()
    {
        switch (Type)
        {
            case BonusType.Coins:
                Player.Instance.Coins *= Amount;
                break;

            case BonusType.SuperCash:
                Player.Instance.SuperCash += Amount;
                break;

            case BonusType.SkipTime:
                float seconds = 24 * 60 * 60; //  hours * mins * seconds
                TimeSkipper.Instance.SkipTimeAll(seconds);
                Player.Instance.SuperCash += Amount;
                break;

            default:
                break;
        }

        DailyBonusManager.Instance.BonusClaimed();
    }
}
