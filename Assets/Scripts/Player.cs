using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player Instance { get { return instance; } }
    static Player instance = null;

    //-----------------------------------------------

    public double Coins = 0;
    public int SuperCash = 0;

    [SerializeField] TextMeshProUGUI coinText = null;
    [SerializeField] TextMeshProUGUI superCashText = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayerSaveData loadedData = SaveManager.Instance.LoadPlayerData();

        if (loadedData!=null)
        {
            Coins = loadedData.Coins;
            SuperCash = loadedData.SuperCash;
        }
    }

    public void Update()
    {
        coinText.text = MoneyConvertor.Convert(Coins);
        superCashText.text = MoneyConvertor.Convert(SuperCash);

    }

    public PlayerSaveData CreateSaveData()
    {
        PlayerSaveData ret = new PlayerSaveData(Coins,SuperCash);
        return ret;
    }


}
