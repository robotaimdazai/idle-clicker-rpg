using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TycoonChest : MonoBehaviour
{
    public Reward[] Rewards;
    public int RequiredMedals = 25;

    //Inspector Elements
    [Header("Settings")]
    [SerializeField] TextMeshProUGUI medalsText;
    [SerializeField] Image fill = null;

    //private members
    int currentRewardIndex = 0;

    private void Update()
    {
        if (medalsText)
        {
            medalsText.text = TaskManager.Instance.EarnedMedals + "/" + RequiredMedals.ToString();
        }
        if (fill)
        {
            float fillPercentage = (float)TaskManager.Instance.EarnedMedals / (float)RequiredMedals;
            fill.fillAmount = fillPercentage;
        }
        
    }

    public void Open()
    {
        int medalsEarned = TaskManager.Instance.EarnedMedals;
        if (medalsEarned >= RequiredMedals)
        {
            Reward reward = Rewards[currentRewardIndex];

            if (reward)
            {
                reward.Get();
            }

            currentRewardIndex++;

            TaskManager.Instance.EarnedMedals -= RequiredMedals;
        }
    }


}
