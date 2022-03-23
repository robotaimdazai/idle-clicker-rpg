using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GIKIT;
using UnityEngine.UI;
using TMPro;

public class DoubleCashController : MonoBehaviour
{
    static DoubleCashController instance = null;
    public static DoubleCashController Instance { get { return instance; } }

    public int SPrice = 5;
    public int Multiplier = 2;
    public int HoursToAdd = 4;
    public int TotalHours = 24;

    [SerializeField] Button buyButton = null;
    [SerializeField] TextMeshProUGUI earnedTimeText = null;
    [SerializeField] Image fill;

    float earnedTimeInSeconds = 0;
    bool isActive = false;

    private void Start()
    {
        DoubleCashSaveData loadedData = SaveManager.Instance.LoadDoubleCashData();
        if (loadedData!=null)
        {
            earnedTimeInSeconds = loadedData.EarnedTimeInSeconds;
            isActive = loadedData.IsActive;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Player.Instance.SuperCash >= SPrice)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
        if (isActive)
        {
            earnedTimeText.gameObject.SetActive(true);
            earnedTimeInSeconds -= Time.deltaTime;
            earnedTimeText.text = TimeConvertor.ConvertFromSeconds(earnedTimeInSeconds);
            int earnedTimeInHours = Mathf.CeilToInt(earnedTimeInSeconds/3600f);
            float percentage = (float)earnedTimeInHours / (float)TotalHours;
            fill.fillAmount = percentage;
            if (earnedTimeInSeconds<=0)
            {
                earnedTimeInSeconds = 0;

                // Halfing the profit again after earned time is expired
                MultiplyProfitAll(0.5f);

                isActive = false;
            }
        }
        else
        {
            earnedTimeText.gameObject.SetActive(false);
        }
    }


    public void Buy()
    {
        if (Player.Instance.SuperCash >= SPrice )
        {
            // double profit here if it was never activated before
            if (!isActive)
            {
                MultiplyProfitAll(Multiplier);

                isActive = true;
            }

            // add EarnedTime

            earnedTimeInSeconds += HoursToAdd * 3600;
            int earnedTimeInHours = (int)earnedTimeInSeconds / 3600; //converting earnedTime from seconds to hours for comparison with total hours
            if (earnedTimeInHours > TotalHours)
            {
                earnedTimeInSeconds = TotalHours * 3600;
            }
            Player.Instance.SuperCash -= SPrice;
        }
    }

    //------------Helpers--------------

    private void MultiplyProfit(Business business, float multiplier)
    {
         business.earningCurve *= multiplier;
    }

    private void MultiplyProfitAll(float multiplier)
    {
        foreach (Transform item in BusinessHolder.Instance.transform)
        {
            Business business = item.GetComponent<Business>();
            if (business)
            {
                MultiplyProfit(business, multiplier);
            }
        }
    }

    public DoubleCashSaveData CreateSaveData()
    {
        DoubleCashSaveData saveData = new DoubleCashSaveData(isActive,earnedTimeInSeconds);
        return saveData;
    }
}
