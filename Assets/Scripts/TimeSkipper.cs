using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeSkipper : MonoBehaviour
{
    static TimeSkipper instance = null;
    public static TimeSkipper Instance { get { return instance; } }

    [SerializeField] Transform businessPanel = null;

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

    public double SkipTimeAll(float seconds)
    {
        double ret = 0;
        if (!businessPanel)
        {
            return ret ;
        }

        foreach (Transform item in businessPanel)
        {
            Business business = item.GetComponent<Business>();
            if (business)
            {
                if (business.IsPurchased)
                {
                   ret+= SkipTime(business, seconds);
                }
                
            }
        }

        return ret;
    }

    private double SkipTime(Business business, float seconds)
    {
        double ret = 0;
        if (!business)
        {
            return ret;
        }
    
        double profit = (business.Earning/business.EarnTime) * seconds;
        ret = profit;

        //adding reward coins with player's current coins 
        Player.Instance.Coins += profit;
        return ret;
        
    }
}
