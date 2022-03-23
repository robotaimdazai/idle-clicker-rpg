using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public bool IsPurchased { get; private set; } = false;
    public Business TargetBusiness = null;
    public string Id;

    [SerializeField] int multiplier = 1;
    [SerializeField] double price = 0;

    [Header("UI Config")]
    [SerializeField] TextMeshProUGUI detailText = null;
    [SerializeField] TextMeshProUGUI priceText = null;
    [SerializeField] Button buyButton = null;

    private void Start()
    {
        UpgradeSaveData loadedData = SaveManager.Instance.LoadUpgradesData(Id);
        if (loadedData!=null)
        {
            if (loadedData.IsPurchased)
            {
                gameObject.SetActive(false);
            }

        }

        if (!TargetBusiness)
        {
            return;
        }

        if (detailText)
        {
            detailText.text = TargetBusiness.Name + " profit x" + multiplier;
        }
        if (priceText)
        {
            priceText.text = MoneyConvertor.Convert(price);
        }
    }

    private void Update()
    {
        if (!IsPurchased)
        {
            if (buyButton)
            {
                if (Player.Instance.Coins>=price)
                {
                    buyButton.interactable = true;
                }
                else
                {
                    buyButton.interactable = false;
                }
            }
        }
    }

    public void Buy()
    {
        if (Player.Instance.Coins >= price)
        {
            if (TargetBusiness)
            {
                Player.Instance.Coins -= price;
                TargetBusiness.Earning *= multiplier;
                IsPurchased = true;
                gameObject.SetActive(false);
            }
        }
    }

    public UpgradeSaveData CreateSaveData()
    {
        UpgradeSaveData ret = new UpgradeSaveData(IsPurchased);
        return ret;
    }

    

}
