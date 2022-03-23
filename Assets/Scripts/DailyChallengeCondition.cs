
using UnityEngine;

public abstract class DailyChallengeCondition
{
    public abstract void Init(DailyChallenge dailyChallenge);
    public abstract bool Check(DailyChallenge dailyChallenge);

}
