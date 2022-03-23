using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GIKIT;

public class Manager : MonoBehaviour
{
    public string Name;
    public double Price;
    public string Description;
    public bool IsPurchased = false;
    public BusinessID TargetBusinessID;
    public Business TargetBusiness;
    public Sprite Sprite = null;

    [Header("Actions")]
    public ManagerAction[] Actions;

    [Header("Slot Config")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Button buyButton;
    [SerializeField] Image image;

    ParticleSpawner particleSpawner = null;
     

    
    private void Start()
    {
        particleSpawner = GetComponent<ParticleSpawner>();

        SetData(Name,Description,Price,Sprite);

        //Loading saved data into manager
        ManagerSaveData loadedData = SaveManager.Instance.LoadManagerData(Name);
        if (loadedData!=null)
        {
            IsPurchased = loadedData.IsPurchased;
            if (IsPurchased)
            {
                buyButton.interactable = false;
                gameObject.SetActive(false);
                TargetBusiness.SetManager(TargetBusiness.FreeSlot, this);
            }
        }
    }

    private void Update()
    {

        if (!IsPurchased)
        {
            if (Player.Instance.Coins >= Price && TargetBusiness.IsPurchased)
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
        }
        
    }

    public void Buy()
    {
        IsPurchased = true;
        buyButton.interactable = false;
        gameObject.SetActive(false);
        TargetBusiness.SetManager(TargetBusiness.FreeSlot,this);
        TargetBusiness.Automate();
        particleSpawner.Spawn();


    }

    //Helper

    public void SetData(string name, string description, double price, Sprite sprite)
    {
        nameText.text = name;
        descriptionText.text = description;
        priceText.text = MoneyConvertor.Convert(price);
        image.sprite = sprite;
        gameObject.name = "Manager (" + TargetBusinessID.ToString()+")";
    }

    public ManagerSaveData CreateSaveData()
    {
        ManagerSaveData ret = null;
        ret = new ManagerSaveData(IsPurchased);
        return ret;
    }

}
