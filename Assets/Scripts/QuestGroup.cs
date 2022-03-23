using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestGroup : MonoBehaviour
{
    public Business TargetBusiness;
    public string Id;
    public int ActiveQuest = 0;

    [SerializeField] GameObject questInfoPrefab;
    [SerializeField] Transform questInfoContentPanel;
    [Header("UI Components")]
    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] TextMeshProUGUI targetLevelText;
    [SerializeField] TextMeshProUGUI rewardText;

    public Quest[] Quests;

    private void Start()
    {
        QuestGroupSaveData loadedData = SaveManager.Instance.LoadQuestsData(Id);
        if (loadedData!=null)
        {
            Quests = loadedData.Quests;
            ActiveQuest = loadedData.ActiveQuest;
        }
    }


    private void Update()
    {
        // dont process quests if all quests  have been completed

        if (Quests.Length<=0 || TargetBusiness==null || (ActiveQuest == Quests.Length - 1 && Quests[ActiveQuest].IsCompleted))
        {
            return;
        }


        Quest quest = Quests[ActiveQuest];

        // telling business about active Quest Target
        TargetBusiness.SetLevelUpTarget(quest.TargetLevel);

        if (!quest.IsCompleted)
        {
            if (TargetBusiness != null)
            {
                if (quest.TargetLevel == TargetBusiness.Level)
                {
                    Notification notification = new Notification(TargetBusiness.Sprite, "", 3f);
                    quest.IsCompleted = true;
                    if (quest.Type == Quest.QuestType.Profit)
                    {
                        TargetBusiness.Earning *= quest.TargetMultiplier;
                        notification.Text = "Profit of " + TargetBusiness.Name + " x" + quest.TargetMultiplier;
                    }
                    else if (quest.Type == Quest.QuestType.Speed)
                    {
                        TargetBusiness.EarnTime /= quest.TargetMultiplier;
                        notification.Text = "Speed of " + TargetBusiness.Name + " x" + quest.TargetMultiplier;
                    }
                    //acquiring next quest index
                    ActiveQuest++;

                    if (ActiveQuest>Quests.Length-1)
                    {
                        ActiveQuest = Quests.Length - 1;
                    }
                    //Show Notification here
                    NotificationManager.Instance.ShowNotification(notification);



                }
            }
        }

        if (currentLevelText)
        {
            currentLevelText.text = TargetBusiness.Level.ToString();
        }
        if (targetLevelText)
        {
            targetLevelText.text = quest.TargetLevel.ToString();
        }
        if (rewardText)
        {
            if (quest.Type == Quest.QuestType.Speed)
            {
                rewardText.text = "Speed x" + quest.TargetMultiplier;
            }
            else if (quest.Type == Quest.QuestType.Profit)
            {
                rewardText.text = "Profit x" + quest.TargetMultiplier;
            }
        }

    }

    public void GenerateAllQuests()
    {
        if (questInfoContentPanel)
        {
            foreach (Transform item in questInfoContentPanel)
            {
                Destroy(item.gameObject);
            }

            for (int i = 0; i < Quests.Length; i++)
            {
                GameObject questInfoObject = Instantiate(questInfoPrefab, questInfoContentPanel);
                QuestInfo questInfo = questInfoObject.GetComponent<QuestInfo>();
                Quest quest = Quests[i];
                if (questInfo && quest!=null)
                {
                    questInfo.TargetText = quest.TargetLevel.ToString();
                    questInfo.BusinessImage = TargetBusiness.Sprite;
                    if (quest.Type == Quest.QuestType.Speed)
                    {
                        questInfo.RewardText = "Speed x" + quest.TargetMultiplier;
                    }
                    else if (quest.Type == Quest.QuestType.Profit)
                    {
                        questInfo.RewardText = "Profit x" + quest.TargetMultiplier;
                    }
                }
                questInfo.Active = false;
                if (i == ActiveQuest)
                {
                    questInfo.Active = true;
                }
                if (quest.IsCompleted)
                {
                    questInfo.Completed = true;
                }
                

            }
        }
    }

    public QuestGroupSaveData CreateQuestgroupSaveData()
    {
        QuestGroupSaveData ret = new QuestGroupSaveData(Quests,ActiveQuest);
        return ret;

    }

}
