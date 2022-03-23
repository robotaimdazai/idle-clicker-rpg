using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rewards/SuperCashReward")]
public class SupercashReward : Reward
{
    public int Amount = 1;

    public override int Get()
    {
        Player.Instance.SuperCash += Amount;
        return Amount;
    }
}
