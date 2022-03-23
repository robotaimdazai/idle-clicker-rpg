using UnityEngine;

[System.Serializable]
public class Quest 
{
    public enum QuestType { Speed, Profit }
    public QuestType Type;
    public int TargetLevel;
    public int TargetMultiplier;
    public bool IsCompleted;
}
