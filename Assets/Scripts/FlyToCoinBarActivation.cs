using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToCoinBarActivation : MonoBehaviour
{
    Animator animator = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ActivateFlyToCoinBar()
    {
        animator.enabled = false;
        foreach (Transform item in transform)
        {
            FlyToCoinBar coin = item.GetComponent<FlyToCoinBar>();
            if (coin)
            {
                coin.Fly();
            }
        }
    }
}
