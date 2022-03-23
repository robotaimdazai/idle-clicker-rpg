using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using GIKIT;

public class TimeRewardManager : MonoBehaviour
{

    [SerializeField]float minimumSecondsForReward = 60f;    // how much away time is required to get Reward
    [SerializeField] TextMeshProUGUI rewardCoinText;
    [SerializeField] TextMeshProUGUI awayDifferenceText;
    [SerializeField] TextMeshProUGUI x2CoinText;
    [SerializeField] TextMeshProUGUI x5CoinText;
    DateTime currentDateTime;
    DateTime exitDateTime;
    TimeSpan timeDifference;
    float timeDifferenceInSeconds = 0f;
    string LAST_SAVE_DATA_TIME = "LAST_SAVE_DATE_TIME";

    private void Start()
    {
        //if key doesnt exist then return
        if (!PlayerPrefs.HasKey(LAST_SAVE_DATA_TIME))
        {
            return;
        }

        currentDateTime = DateTime.UtcNow;
        exitDateTime = DateTime.Parse(PlayerPrefs.GetString(LAST_SAVE_DATA_TIME));
        timeDifference = currentDateTime - exitDateTime;
        timeDifferenceInSeconds = (float)timeDifference.TotalSeconds;
        awayDifferenceText.text = TimeConvertor.ConvertFromSeconds(timeDifferenceInSeconds);

        if (CanGetReward())
        {
            RewardPlayer();
        }
    }

    private void Update()
    {
        currentDateTime = DateTime.UtcNow;
    }

    private void OnApplicationQuit()
    {
        exitDateTime = currentDateTime;
        PlayerPrefs.SetString(LAST_SAVE_DATA_TIME, exitDateTime.ToString());
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            exitDateTime = currentDateTime;
            PlayerPrefs.SetString(LAST_SAVE_DATA_TIME, exitDateTime.ToString());
        }
        else
        {
            if (CanGetReward())
            {
                RewardPlayer();
                
            }
        }
    }


    public void RewardPlayer()
    {
        double rewardMoney = TimeSkipper.Instance.SkipTimeAll(timeDifferenceInSeconds);
        UIManager.Instance.SwitchScreen(UIScreenType.WelcomeScreen);
        Debug.Log("Rewarding player");
        if (rewardCoinText)
        {
            rewardCoinText.text = MoneyConvertor.Convert(rewardMoney);
        }
        if (x2CoinText)
        {
            x2CoinText.text = MoneyConvertor.Convert(rewardMoney * 2);
        }
        if (x5CoinText)
        {
            x2CoinText.text = MoneyConvertor.Convert(rewardMoney * 5);
        }
    }

    //helpers
    bool CanGetReward()
    {
        bool ret = false;
        bool isTime = false;

        isTime = (timeDifferenceInSeconds >= minimumSecondsForReward) ? true : false;
        bool isAnyBusinessPurchased = BusinessHolder.Instance.IsAnyBusinessPurchased();

        ret = isTime && isAnyBusinessPurchased;
        return ret;
    }


}
