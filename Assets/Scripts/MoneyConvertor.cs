using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using System.Numerics;

public class MoneyConvertor
{

    public static string Convert(double num)
    {
        string ret = "";
        BigInteger t = BigInteger.Parse("999999999999999999999999");
        
        if (num > (double)t || num < (double)-t)
        {
            ret = "$" + num.ToString("0,,,,.####S", CultureInfo.InvariantCulture);
            return ret;
        }

        if (num > 999999999999999999 || num < -999999999999999999)
        {
            ret = "$" + num.ToString("0,,,,.####Q", CultureInfo.InvariantCulture);
            return ret;
        }

        if (num > 999999999999 || num < -999999999999)
        {
            ret = "$" + num.ToString("0,,,,.####T", CultureInfo.InvariantCulture);
            return ret;
        }
        else if (num > 999999999 || num < -999999999)
        {
            ret = "$" + num.ToString("0,,,.###B", CultureInfo.InvariantCulture);
            return ret;
        }
        else if (num > 999999 || num < -999999)
        {
            ret = "$"+ num.ToString("0,,.##M", CultureInfo.InvariantCulture);
            return ret;
        }
        else
        {
            num = (int)num;
            ret = "$"+ num.ToString(CultureInfo.InvariantCulture);
            return ret;
        }

            
    }

  
}
