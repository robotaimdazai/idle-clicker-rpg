using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DailyChallengeManager : MonoBehaviour
{
    static DailyChallengeManager instance = null;
    public static DailyChallengeManager Instance { get { return instance; } }

    public DailyChallenge[] DailyChallenges = null;

    [Header("Settings")]
    [SerializeField] TextMeshProUGUI activeChallengeDescriptionText;
    [SerializeField] TextMeshProUGUI activeChallengeAchievedTaskGoalsText;
    [SerializeField] Image progressBarFill = null;
    [SerializeField] TextMeshProUGUI rewardText = null;

    int currentChallengeIndex = 0;
    DailyChallenge activeChallenge = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance!=this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        activeChallenge = DailyChallenges[currentChallengeIndex];
        activeChallenge.Init();
        activeChallengeDescriptionText.text = activeChallenge.Description.ToString();
        activeChallengeAchievedTaskGoalsText.text = activeChallenge.AchievedGoals.ToString() + "/" + activeChallenge.TotalGoals.ToString();
    }

    private void Update()
    {
        if (currentChallengeIndex > DailyChallenges.Length - 1)
        {
            return;
        }

        bool isCurrentTaskCompleted = activeChallenge.CheckStatus();

        if (isCurrentTaskCompleted)
        {
            activeChallenge.GetReward();

            Notification taskCompleteNotification = new Notification(activeChallenge.NotificationSprite, activeChallenge.CompletionMessage, 2f);
            NotificationManager.Instance.ShowNotification(taskCompleteNotification);

            currentChallengeIndex++;

            if (currentChallengeIndex <= DailyChallenges.Length - 1)
            {
                activeChallenge = DailyChallenges[currentChallengeIndex];
                activeChallenge.Init();
            }

            float fillValue = activeChallenge.AchievedGoals / activeChallenge.TotalGoals;
            progressBarFill.fillAmount = fillValue;

            activeChallengeDescriptionText.text = activeChallenge.Description.ToString();
            activeChallengeAchievedTaskGoalsText.text = activeChallenge.AchievedGoals.ToString() + "/" + activeChallenge.TotalGoals.ToString();
            rewardText.text = "x" + activeChallenge.Rewards[0].Get().ToString();

        }
    }

}
