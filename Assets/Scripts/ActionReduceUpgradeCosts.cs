using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ReduceUpgradeCost",menuName ="ManagerActions/UpgradeCostReduction")]
public class ActionReduceUpgradeCosts : ManagerAction
{
    [Range(0,1)]
    public float Percentage = 0.2f;

    public override void Act(Business business)
    {
        business.expandCurve -= business.expandCurve * Percentage;
    }
}
   
