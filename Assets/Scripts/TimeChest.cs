using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeChest : MonoBehaviour
{
    public float TimeInSeconds = 216000;
    public Reward[] Rewards;

    float time = 0f;
    bool isTimeToOpen = false;
    int currentRewardIndex = 0;

    [Header("Settings")]
    [SerializeField] TextMeshProUGUI remainingTimeText = null;


    private void Update()
    {
        if (!isTimeToOpen)
        {
            time += Time.deltaTime;
            float remainingTime = TimeInSeconds - time;
      
            if (remainingTime <= 0)
            {
                isTimeToOpen = true;

            }
            if (remainingTimeText)
            {
                remainingTimeText.text = TimeConvertor.ConvertFromSeconds(remainingTime);
            }
        }
        
    }

    public void Open()
    {
        Reward reward = Rewards[currentRewardIndex];
        if (reward!=null)
        {
            if (currentRewardIndex<=Rewards.Length-1)
            {
                reward.Get();
                time = 0f;
                isTimeToOpen = false;
                currentRewardIndex++;
            }
        }
    }
}
