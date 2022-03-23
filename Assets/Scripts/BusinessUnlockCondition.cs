using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TaskConditions/BusinessUnlockCondition")]
public class BusinessUnlockCondition : TaskCondition
{
    public BusinessID TargetBusinessID;

    Business targetBusiness = null;

    public override void Init(Task task)
    {
        task.TotalGoals = 1;
        task.AchievedGoals = 0;

        foreach (Transform item in BusinessHolder.Instance.transform)
        {
            Business business = item.GetComponent<Business>();
            if (business)
            {
                if (TargetBusinessID == business.ID)
                {
                    targetBusiness = business;
                }
            }

        }
    }

    public override bool Check(Task task)
    {
        bool ret = false;

        if (targetBusiness.IsPurchased)
        {
            ret = true;
            task.AchievedGoals = 1;
        }

        return ret;
    }

    
}
