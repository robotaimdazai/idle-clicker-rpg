using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCoinSpawner : MonoBehaviour
{
    public FlyingCoin FlyingCoin;
    public SpawnPoint[] SpawnPoints;
    public Vector2 TimeMinMax;
    public bool IsStopped = false;

    float spawnTime = 0f;
    float time = 0f;
    float coinLifeTime = 12f;

    private void Start()
    {
        spawnTime = Random.Range(TimeMinMax.x,TimeMinMax.y);
        FlyingCoin.gameObject.SetActive(false);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (IsStopped)
        {
            if (time >= coinLifeTime)
            {
                FlyingCoin.gameObject.SetActive(false);
                IsStopped = false;
                time = 0f;
            }
        }
        else
        {
            if (time >= spawnTime)
            {
                int randomNumber = Random.Range(0, SpawnPoints.Length - 1);
                SpawnPoint spawnPoint = SpawnPoints[randomNumber];
                FlyingCoin.transform.position = spawnPoint.transform.position;

                if (spawnPoint.Side == SpawnPoint.SideType.Left)
                {
                    FlyingCoin.Direction = 1;
                }
                else
                {
                    FlyingCoin.Direction = -1;
                }

                FlyingCoin.gameObject.SetActive(true);
                FlyingCoin.GetComponent<SpriteRenderer>().enabled = true;
                FlyingCoin.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                FlyingCoin.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

                time = 0f;
                spawnTime = Random.Range(TimeMinMax.x, TimeMinMax.y);
                IsStopped = true;
            }
        }

        
    }
}
