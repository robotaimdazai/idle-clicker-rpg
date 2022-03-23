using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TimeReduction",menuName ="ManagerActions/ TimeReduction")]
public class ActionReduceTime : ManagerAction
{
    public float timeReduction = 1f;

    public override void Act(Business business)
    {
        business.EarnTime -= timeReduction;
    }
}
