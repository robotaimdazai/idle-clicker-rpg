using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OnEnableBusinessDataSetter : MonoBehaviour
{
    public void OnEnable()
    {
        Business business = GetComponent<Business>();
        if (Application.isEditor && !Application.isPlaying)
        {
            business.SetData(business.Name,business.Sprite, business.EarnTime, business.Earning, business.ExpandPrice, 
                business.Level, business.Art, business.ProductSprite, business.Price, business.IsPurchased);

            Debug.Log("Setting Data");

        }
    }
}
