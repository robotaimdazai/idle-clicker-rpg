using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerOderer : MonoBehaviour
{


    static ManagerOderer instance = null;
    public static ManagerOderer Instance { get { return instance; } }

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
    }

    public void Order(BusinessID id)
    {
       

        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.childCount-1; j++)
            {
                Manager currentManager = transform.GetChild(j).GetComponent<Manager>();
                Manager nextManager = transform.GetChild(j+1).GetComponent<Manager>();

                if (currentManager.TargetBusinessID != id && nextManager.TargetBusinessID == id)
                {
                    nextManager.transform.SetSiblingIndex(j);
                    currentManager.transform.SetSiblingIndex(j + 1);
                }
            }
        }
    }

    public void OrderByCoins()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.childCount - 1; j++)
            {
                Manager currentManager = transform.GetChild(j).GetComponent<Manager>();
                Manager nextManager = transform.GetChild(j + 1).GetComponent<Manager>();

                if (currentManager.Price > Player.Instance.Coins && nextManager.Price <= Player.Instance.Coins)
                {
                    nextManager.transform.SetSiblingIndex(j);
                    currentManager.transform.SetSiblingIndex(j + 1);
                }
            }
        }
    }

}
