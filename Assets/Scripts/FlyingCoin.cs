using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCoin : MonoBehaviour
{
    public float Direction = 1;
    public float Speed =1;
    public float Amplitude=1f;
    public float Frequency = 1f;

    float t = 0;

    void Update()
    {
        t += Time.deltaTime;

        float x = Direction;
        float y = Amplitude* Mathf.Sin(t * Frequency);
        Vector3 direction = new Vector3(x,y,0f);
        transform.position += direction * Speed * Time.deltaTime;

    }
}
