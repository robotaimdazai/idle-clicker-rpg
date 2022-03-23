using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GIKIT;
using TMPro;
using UnityEngine.UI;

public class IncomeBooster : MonoBehaviour
{
    public int SPrice = 1;
    public int Multiplier = 1;

    [SerializeField] TextMeshProUGUI totalMultiplierText = null;
    [SerializeField] GameObject supercashSpendParticles = null;
    [SerializeField] AudioClip supercashSound = null;

    int totalMultiplier = 0;
    Button buyButton = null;

    private void Start()
    {
        buyButton = GetComponent<Button>();
    }

    private void Update()
    {
        if (Player.Instance.SuperCash >= SPrice)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }


    public void Buy()
    {
        if (Player.Instance.SuperCash >= SPrice)
        {
            Player.Instance.SuperCash -= SPrice;
            ApplyIncomeBoostAll();
            //UIManager.Instance.SwitchScreen(UIScreenType.GameScreen);
            totalMultiplier += Multiplier;
            totalMultiplierText.text = "x" + totalMultiplier;
            GameObject spawnedParticles = Instantiate(supercashSpendParticles,transform.position,Quaternion.identity);
            float lifeTime = 2f;
            Destroy(spawnedParticles, lifeTime);
            MusicManager.Instance.PlaySFX(supercashSound);
            
        }
    }

    private void ApplyIncomeBoostAll()
    {
        Transform businessHolderTransform = BusinessHolder.Instance.transform;
        foreach (Transform item in businessHolderTransform)
        {
            Business business = item.GetComponent<Business>();
            if (business)
            {
                ApplyIncomeBoost(business,Multiplier);
            }
        }
    }

    private void ApplyIncomeBoost(Business business, int multiplier)
    {
        business.EarningMultiplier = multiplier;
    }
}
