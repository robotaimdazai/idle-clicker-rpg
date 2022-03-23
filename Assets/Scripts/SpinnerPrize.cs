using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerPrize : MonoBehaviour
{
    public enum PrizeType { Coins, Supercash, SkipTime }
    public PrizeType Type;

    public int Amount = 0;
}
