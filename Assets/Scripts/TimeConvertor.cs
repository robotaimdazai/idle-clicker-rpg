using System;
using UnityEngine;

public class TimeConvertor 
{
    public static string ConvertFromSeconds(float t)
    {
        if (t<1)
        {
            t = 1;
        }
        string answer = "";
        TimeSpan timeSpan = TimeSpan.FromSeconds(t);

            answer = string.Format("{0:D2}:{1:D2}",
            timeSpan.Minutes,
            timeSpan.Seconds);

        if (timeSpan.Hours > 0)
        {
            answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
            timeSpan.Hours,
            timeSpan.Minutes,
            timeSpan.Seconds);
        }

        if (timeSpan.Days>0)
        {
            answer = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}",
            timeSpan.Days,
            timeSpan.Hours,
            timeSpan.Minutes,
            timeSpan.Seconds);
        }

        return answer;

    }

      


}
