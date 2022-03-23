using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    public int PrizeAmount { get { return prizeAmount; } } // returns the prize amount under the needle
    public SpinnerPrize.PrizeType PrizeType { get { return prizeType; } }

    int prizeAmount = 0;
    SpinnerPrize.PrizeType prizeType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpinnerPrize spinnerPrize = collision.GetComponent<SpinnerPrize>();
        if (spinnerPrize)
        {
            prizeAmount = spinnerPrize.Amount;
            prizeType = spinnerPrize.Type;
        }
    }
}
