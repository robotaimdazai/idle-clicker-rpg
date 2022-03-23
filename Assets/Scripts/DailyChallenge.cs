
using UnityEngine;

[System.Serializable]
public class DailyChallenge
{
    public string Description;
    public string CompletionMessage;
    public Sprite NotificationSprite = null;
    public DailyChallengeCondition Condition;
    public bool IsCompleted = false;
    public Reward[] Rewards;

    [HideInInspector] public int AchievedGoals = 0;
    [HideInInspector] public int TotalGoals = 0;

    public void Init()
    {
        if (Condition == null) { return; }

        Condition.Init(this);
    }

    public bool CheckStatus()
    {
        if (Condition == null) { return false; }

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
