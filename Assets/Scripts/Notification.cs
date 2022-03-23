using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification
{
    public Sprite Sprite;
    public string Text;
    public float Time;

    public Notification(Sprite sprite, string text, float time)
    {
        this.Sprite = sprite;
        this.Text = text;
        this.Time = time;
    }
}
