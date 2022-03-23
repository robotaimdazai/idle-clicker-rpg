using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusManager : MonoBehaviour
{
    public static DailyBonusManager Instance { get { return instance; } }
    static DailyBonusManager instance = null;

    public DailyBonus[] DailyBonuses = null;

    int currentBonusIndex = 0;

    float remainingSeconds = 86400;

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
        ActivateBonus(currentBonusIndex);
    }

    private void Update()
    {
        remainingSeconds -= Time.deltaTime;

        if (remainingSeconds<=0)
        {
            currentBonusIndex++;
            if (currentBonusIndex > 6)
            {
                currentBonusIndex = 0;
            }
            remainingSeconds = 86400;
            ActivateBonus(currentBonusIndex);
        }
    }

    private void ActivateBonus(int index)
    {
        foreach (DailyBonus item in DailyBonuses)
        {
            item.GetComponent<Button>().interactable = false;
        }

        DailyBonuses[currentBonusIndex].GetComponent<Button>().interactable = true;
    }

    public void BonusClaimed()
    {
        remainingSeconds = 86400;
    }
}
