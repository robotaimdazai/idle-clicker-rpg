using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StockExchanger : MonoBehaviour
{
    [Range(0,1)]
    public float ProfitPercentage = 0.8f;

    [SerializeField] TextMeshProUGUI interactText = null;

    [Header("Sidebar settings")]
    [SerializeField] TextMeshProUGUI bidText;
    [SerializeField] TextMeshProUGUI investedRateText;
    [SerializeField] TextMeshProUGUI liveCoinsText;
    [SerializeField] TextMeshProUGUI currentRateText;
    [SerializeField] TextMeshProUGUI netRateText;
    [SerializeField] TextMeshProUGUI remainingTimeText = null;
    [SerializeField] Button skipTimeButton = null;
    [SerializeField] Transform bidLine = null;
    [SerializeField] Button investButton = null;

    Graph graph = null;
    double bidAmount = 0;
    float investRate = 0f;
    float sellRate = 0f;
    bool hasInvested = false;
    float timeLimit = 30f;
    float time = 0f;
    bool isTimeBeingSkipped = false;
    float timeToSkip = 10f;
    float bidPercentage = 0.2f;

    private void Start()
    {
        graph = GetComponent<Graph>();
        bidText.text = MoneyConvertor.Convert(0);
        investedRateText.text = 0 + "";
        netRateText.text = 0 + "";
        bidAmount = 0;

        if (bidLine)
        {
            bidLine.gameObject.SetActive(false);
        }
      
    }

    private void Update()
    {
        currentRateText.text = string.Format("{0:0.0}", graph.Rate * 100);
        liveCoinsText.text = MoneyConvertor.Convert(Player.Instance.Coins);
        bidText.text = MoneyConvertor.Convert(bidAmount);

        
        float  netRate = graph.Rate - investRate;
        netRateText.text = string.Format("{0:0.0}", netRate* 100f);

        if (skipTimeButton)
        {
            if (!hasInvested || isTimeBeingSkipped)
            {
                skipTimeButton.interactable = false;
            }
            else
            {
                skipTimeButton.interactable = true;
            }
        }
        
        
        

        //loss case
        if (netRate < 0)
        {
            netRateText.color = Color.red;
            graph.RateText.text = "-100%";
            graph.RateText.color = Color.red;
        }
        //profit case
        else
        {
            
            netRateText.color = Color.green;
            graph.RateText.text = "+" + (ProfitPercentage * 100f).ToString() + "%";
            graph.RateText.color = Color.green;

            if (netRate == 0)
            {
                netRateText.color = Color.white;
                
            }
        }

        if (hasInvested)
        {
            if (isTimeBeingSkipped)
            {
                time += Time.deltaTime * timeToSkip;
            }
            else
            {
                time += Time.deltaTime;
            }

            float remainingTime = timeLimit - time;

            if (time >= timeLimit)
            {
                time = 0f;
                remainingTime = timeLimit;
                Sell();

            }

            if (remainingTime <= 10f)
            {
                remainingTimeText.color = Color.red;
            }
            else
            {
                remainingTimeText.color = Color.green;
            }
            remainingTimeText.text = TimeConvertor.ConvertFromSeconds(remainingTime);
            investButton.interactable = false;
            graph.RateText.gameObject.SetActive(true);

        }
        else
        {
            investButton.interactable = true;
            graph.RateText.gameObject.SetActive(false);
        }

        if (bidAmount <= 0)
        {
            investButton.interactable = false;
        }
        else
        {
            investButton.interactable = true;
        }
        
    }

    public void RaiseBid()
    {
        double amountToBid = Player.Instance.Coins * bidPercentage;
        bidAmount += amountToBid;
        if (bidAmount >= Player.Instance.Coins)
        {
            bidAmount = Player.Instance.Coins;
        }
        
    }

    public void LowerBid()
    {
        double amountToBid = Player.Instance.Coins * bidPercentage;
        bidAmount -= amountToBid;
        if (bidAmount <=0)
        {
            bidAmount = 0;
        }
    }

    private void Invest()
    {
        if (bidAmount<=0)
        {
            return;
        }

        hasInvested = true;
        graph.BidLinePercentage = graph.Rate;
        Player.Instance.Coins -= bidAmount;
        investRate = graph.Rate;
        bidText.text = MoneyConvertor.Convert(bidAmount);
        investedRateText.text = string.Format("{0:0.0}", investRate * 100);
        bidLine.gameObject.SetActive(true);
        bidLine.position = new Vector2(bidLine.transform.position.x,graph.CurrentNodePosition.y);
    }

    private void Sell()
    {
        hasInvested = false;
        sellRate = graph.Rate;
        float rateDifference = sellRate - investRate;

        //Profit case
        if (rateDifference > 0)
        {
            double coinsEarned = bidAmount * ProfitPercentage;
            Player.Instance.Coins += bidAmount + coinsEarned;
        }
 
        bidText.text = MoneyConvertor.Convert(0);
        investedRateText.text = 0 + "";
        netRateText.text = 0 + "";
        investRate = 0;
        time = 0F;
        remainingTimeText.text = TimeConvertor.ConvertFromSeconds(30);
        bidAmount = 0;
        graph.BidLinePercentage = 1f;
        bidLine.gameObject.SetActive(false);

    }

    public void Interact()
    {
        if (!hasInvested)
        {
            Invest();
        }
    }

    public void SkipTime()
    {
        isTimeBeingSkipped = true;
        graph.SpeedMultiplier = 10f;
        StartCoroutine(SkipTimeCoroutine());
    }

    IEnumerator SkipTimeCoroutine()
    {
        yield return new WaitForSeconds(1f);
        isTimeBeingSkipped = false;
        graph.SpeedMultiplier = 1f;
    }
}
