using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessHolder : MonoBehaviour
{
    public static BusinessHolder Instance { get { return instance; } }
    static BusinessHolder  instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance!= this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public bool IsAnyBusinessPurchased()
    {
        bool ret = false;
        foreach (Transform item in transform)
        {
            Business business = item.GetComponent<Business>();
            if (business)
            {
                if (business.IsPurchased)
                {
                    ret = true;
                }
            }
        }
        return ret;
    }

}
