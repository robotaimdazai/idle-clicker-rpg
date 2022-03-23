using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Spinner : MonoBehaviour
{
    public enum SpinnerType {Free, Paid }

    public Transform Wheel = null;
    public Needle Needle = null;
    public SpinnerType Type;

    [SerializeField]  Button spinButton = null;
    [SerializeField]  TextMeshProUGUI remainingTimeText = null;

    float reducer = 0;
    float multiplier = 0;
    bool round1 = false;
    bool isStoped = true;
    bool isRewarded = false;
    bool isAvailable = true;
    DateTime lastSpinDateTime;
    DateTime currentDateTime;
    TimeSpan timeSpan;

    private void Update()
    {
        currentDateTime = DateTime.UtcNow;
        if (!isAvailable)
        {
            spinButton.interactable = false;

            if (Type == SpinnerType.Free)
            {
                timeSpan = currentDateTime - lastSpinDateTime;
                double totalTimeInHours = timeSpan.TotalHours;
                if (totalTimeInHours >= 24)
                {
                    isAvailable = true;
                }

                
                remainingTimeText.gameObject.SetActive(true);
                double totalTimeInSeconds = timeSpan.TotalSeconds;
                double secondsIn24Hours = 86400;
                double remainingTime = secondsIn24Hours - totalTimeInSeconds;
                remainingTimeText.text = TimeConvertor.ConvertFromSeconds((float)remainingTime);
            }
           
        }
        else
        {
            spinButton.interactable = true;
            remainingTimeText.gameObject.SetActive(false);
        }
        
        
    }



    // Update is called once per frameQ
    void FixedUpdate()
    {
        if (isStoped == true)
        {
            if (Type == SpinnerType.Paid)
            {
                isAvailable = true;
            }
            return;
        }

        if (multiplier > 0)
        {
            Wheel.transform.Rotate(Vector3.forward, 1 * multiplier);
        }
        else
        {
            isStoped = true;

            if (!isRewarded)
            {
                RewardPlayer();
            }

        }

        if (multiplier < 20 && !round1)
        {
            multiplier += 0.1f;
        }
        else
        {
            round1 = true;
        }

        if (round1 && multiplier > 0)
        {
            multiplier -= reducer;
        }
    }


    public void Spin()
    {
        multiplier = 1;
        reducer = UnityEngine.Random.Range(0.01f, 0.5f);
        round1 = false;
        isStoped = false;
        isRewarded = false;
        lastSpinDateTime = DateTime.UtcNow;
        isAvailable = false;
    }

    public void RewardPlayer()
    {
        isRewarded = true;

        switch (Needle.PrizeType)
        {
            case SpinnerPrize.PrizeType.Coins:
                Player.Instance.Coins = Player.Instance.Coins * Needle.PrizeAmount;
                Debug.Log("Coins added");
                break;

            case SpinnerPrize.PrizeType.Supercash:
                Player.Instance.SuperCash += Needle.PrizeAmount;
                Debug.Log("Super cash added");
                break;

            case SpinnerPrize.PrizeType.SkipTime:
                TimeSkipper.Instance.SkipTimeAll(Needle.PrizeAmount);
                Debug.Log("skip time");
                break;

            default:
                Debug.Log("default");
                break;
        }

        Debug.Log("Player has been rewarded");
        

    }
}


