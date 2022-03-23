
using UnityEngine;

[System.Serializable]
public class QuestGroupSaveData
{
    public Quest[] Quests;
    public int ActiveQuest;

    public QuestGroupSaveData() { }

    public QuestGroupSaveData(Quest[] quests, int activeQuest)
    {
        Quests = quests;
        ActiveQuest = activeQuest;
    }

}
