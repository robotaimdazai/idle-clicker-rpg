using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public string Description;
    public string CompletionMessage;
    public Sprite NotificationSprite = null;
    public TaskCondition Condition;
    public bool IsCompleted = false;
    public Reward[] Rewards;
    public int RewardMedals = 5;

    [HideInInspector]public int AchievedGoals = 0;
    [HideInInspector]public int TotalGoals = 0;

    public void Init()
    {
        Condition.Init(this);
    }

    public bool CheckStatus()
    {
        IsCompleted = Condition.Check(this);
        return IsCompleted;
    }

    public void GetReward()
    {
        foreach (Reward reward in Rewards)
        {
            reward.Get();
        }
    }

    

    
}
