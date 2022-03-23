using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    static TaskManager instance = null;
    public static TaskManager Instance { get { return instance; } }
    public Task[] Tasks;
    public int EarnedMedals { set { earnedMedals = value; } get { return earnedMedals; } }

    [Header("Settings")]
    [SerializeField] TextMeshProUGUI activeTaskDescriptionText;
    [SerializeField] TextMeshProUGUI activeTaskAchievedTaskGoalsText;
    [SerializeField] Image progressBarFill = null;
    [SerializeField] TextMeshProUGUI rewardText;

    int currentTaskIndex = 0;
    Task activeTask = null;
    int earnedMedals = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance!=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        activeTask = Tasks[currentTaskIndex];
        activeTask.Init();
        activeTaskDescriptionText.text = activeTask.Description.ToString();
        activeTaskAchievedTaskGoalsText.text = activeTask.AchievedGoals.ToString() + "/" + activeTask.TotalGoals.ToString();
    }

    private void Update()
    {
        if (currentTaskIndex > Tasks.Length-1)
        {
            return;
        }

        bool isCurrentTaskCompleted = activeTask.CheckStatus();

        if (isCurrentTaskCompleted)
        {
            activeTask.GetReward();
            earnedMedals += activeTask.RewardMedals; 
            
            Notification taskCompleteNotification = new Notification(activeTask.NotificationSprite,activeTask.CompletionMessage,2f);
            NotificationManager.Instance.ShowNotification(taskCompleteNotification);

            currentTaskIndex++;

            if (currentTaskIndex <= Tasks.Length - 1)
            {
                activeTask = Tasks[currentTaskIndex];
                activeTask.Init();
            }

            float fillValue = activeTask.AchievedGoals / activeTask.TotalGoals;
            progressBarFill.fillAmount = fillValue;

            activeTaskDescriptionText.text = activeTask.Description.ToString();
            activeTaskAchievedTaskGoalsText.text = activeTask.AchievedGoals.ToString() + "/" + activeTask.TotalGoals.ToString();
            rewardText.text = "x" + activeTask.Rewards[0].Get().ToString();

        }
    }

    
}
