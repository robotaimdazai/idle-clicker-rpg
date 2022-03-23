using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiplierChanger : MonoBehaviour
{
    static MultiplierChanger instance = null;
    public static MultiplierChanger Instance { get { return instance; } }

    [SerializeField] Transform businessContainer = null;
    [SerializeField] TextMeshProUGUI multiplierText = null;

    int currentMultiplier = 1;

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
        if (multiplierText)
        {
            if (currentMultiplier < 1000)
            {
                multiplierText.text = "x" + currentMultiplier;
            }
            else
            {
                multiplierText.text = "Max";
            }

        }
    }

    public void ChangeMultiplier()
    {
        currentMultiplier *= 10;

        // multiplier becomes 1000 at MAX level, when it passes 100 reset it back to 0
        if (currentMultiplier > 1000)
        {
            currentMultiplier = 1;
        }

        if (multiplierText)
        {
            if (currentMultiplier < 1000)
            {
                multiplierText.text = "x" + currentMultiplier;
            }
            else
            {
                multiplierText.text = "Max";
            }

        }

        ApplyMultiplier();
    }

    public void ApplyMultiplier()
    {
        if (!businessContainer)
        {
            return;
        }

        bool isMaxed = currentMultiplier < 1000 ? false : true;

        foreach (Transform item in businessContainer)
        {
            Business business = item.GetComponent<Business>();

            if (business)
            {
                if (isMaxed)
                {
                    CalculateMaxMultiplier(business);
                }
                else
                {
                    business.ExpandMultiplier = currentMultiplier;
                }
            }

        }
    }


    private void CalculateMaxMultiplier(Business business)
    {
        business.ExpandMultiplier = 1;
        StartCoroutine(MaxExpandMultiplierCoroutine(business));
    }

    IEnumerator MaxExpandMultiplierCoroutine(Business business)
    {
        double newExpandPrice = business.ExpandPrice;
        double sum = 0;
        while (true)
        {
            if (sum > Player.Instance.Coins)
            {
                break;
            }

            newExpandPrice += (newExpandPrice * business.ExpandCurve);

            if (sum + newExpandPrice > Player.Instance.Coins)
            {
                if (business.ExpandMultiplier > 1)
                {
                    business.ExpandMultiplier--;
                }
                break;
            }

            sum += newExpandPrice;
            business.ExpandMultiplier++;
            yield return null;
        }
    }

    public bool IsMaxed()
    {
        bool ret = (currentMultiplier == 1000) ? true:false;
        return ret;
    } 

}
