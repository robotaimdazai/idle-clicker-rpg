using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GIKIT;


public class TapReward : MonoBehaviour
{
    public double coins = 1000;

    [SerializeField] GameObject collectParticles = null;
    [SerializeField] AudioClip coinCollectionSFX = null;

    public void RewardCoins()
    {
        float delaySeconds = 0.8f;
        StartCoroutine(RewardPlayerWithDelay(delaySeconds));
        if (collectParticles)
        {
            GameObject spawnedParticles = Instantiate(collectParticles, transform.position, Quaternion.identity, UIManager.Instance.transform);
            float lifeTime = 2f;
            Destroy(spawnedParticles, lifeTime);
        }
        MusicManager.Instance.PlaySFX(coinCollectionSFX);

        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;

        
        
    }

    IEnumerator RewardPlayerWithDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Player.Instance.Coins += coins;
    }
}
