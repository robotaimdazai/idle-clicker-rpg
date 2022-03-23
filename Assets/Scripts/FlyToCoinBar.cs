using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToCoinBar : MonoBehaviour
{
    public float LerpTime =1f;

    Vector3 startPosition = Vector3.zero;
    Vector3 targetPosition = Vector3.zero;
    float timeLerped = 0f;

    bool fly = false;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = CoinBar.Instance.transform.position;
        LerpTime = Random.Range(1f,2f);

    }

    private void Update()
    {
        if (fly)
        {
            timeLerped += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, timeLerped / LerpTime);
           
        }
        
    }

    public void Fly()
    {
        fly = true;
    }
    
}
