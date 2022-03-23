using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveFilePaths
{
    public readonly static string BUSINESS_SAVE = "BusinessSaveFile";
    public readonly static string PLAYER_SAVE = "PlayerSaveFile";
    public readonly static string MANAGERS_SAVE = "ManagersSaveFile";
    public readonly static string UPGRADES_SAVE = "UpgradesSaveFile";
    public readonly static string QUESTS_SAVE = "QuestSaveFile";
    public readonly static string DOUBLE_CASH_SAVE = "DoubleCashSaveFile";
}

public class SaveManager : MonoBehaviour
{
    static SaveManager instance = null;
    public static SaveManager Instance { get { return instance; } }

    Dictionary<BusinessID, BusinessSaveData> businessDictionary = new Dictionary<BusinessID, BusinessSaveData>();
    Dictionary<string, ManagerSaveData> managersDictionary = new Dictionary<string, ManagerSaveData>();
    Dictionary<string, UpgradeSaveData> upgradesDictionary = new Dictionary<string, UpgradeSaveData>();
    Dictionary<string, QuestGroupSaveData> questsGroupDictionary = new Dictionary<string, QuestGroupSaveData>();

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

        ReadBusinessDictionary();
        ReadManagersDictionary();
        ReadUpgradesDictionary();
        ReadQuestsGroupDictionary();
    }


    private void SaveBusinessData(BusinessID id, BusinessSaveData data)
    {
        if (businessDictionary.ContainsKey(id))
        {
            businessDictionary[id] = data;
        }
        else
        {
            businessDictionary.Add(id, data);
        }
    }

    private void SaveQuestGroupData(string id, QuestGroupSaveData data)
    {
        if (questsGroupDictionary.ContainsKey(id))
        {
            questsGroupDictionary[id] = data;
        }
        else
        {
            questsGroupDictionary.Add(id, data);
        }
    }

    private void SaveManagerData(string name, ManagerSaveData data)
    {
        if (managersDictionary.ContainsKey(name))
        {
            managersDictionary[name] = data;
        }
        else
        {
            managersDictionary.Add(name, data);
        }
    }

    private void SaveUpgradesData(string id, UpgradeSaveData data)
    {
        if (upgradesDictionary.ContainsKey(id))
        {
            upgradesDictionary[id] = data;
        }
        else
        {
            upgradesDictionary.Add(id, data);
        }
    }

    private void WritePlayerData(PlayerSaveData data)
    {
        if (data!=null)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = File.Create(Application.persistentDataPath + SaveFilePaths.PLAYER_SAVE))
            {
                bf.Serialize(file,data);
            }
        }
    }

    private void WriteQuestGroupDictionary()
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream file = File.Create(Application.persistentDataPath + SaveFilePaths.QUESTS_SAVE))
        {
            bf.Serialize(file,questsGroupDictionary);
        }
    }

    private void WriteUpgradesDictionary()
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream file = File.Create(Application.persistentDataPath + SaveFilePaths.UPGRADES_SAVE))
        {
            bf.Serialize(file,upgradesDictionary);
        }
    }

    private void WriteDoubleCashData(DoubleCashSaveData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream file = File.Create(Application.persistentDataPath + SaveFilePaths.DOUBLE_CASH_SAVE))
        {
            bf.Serialize(file,data);
        }
    }

    public BusinessSaveData LoadBusinessData(BusinessID id)
    {
        BusinessSaveData ret = null;
        businessDictionary.TryGetValue(id, out ret);
        return ret;
    }

    public QuestGroupSaveData LoadQuestsData(string id)
    {
        QuestGroupSaveData ret = null;
        questsGroupDictionary.TryGetValue(id, out ret);
        return ret;
    }

    public UpgradeSaveData LoadUpgradesData(string id)
    {
        UpgradeSaveData ret = null;
        upgradesDictionary.TryGetValue(id, out ret);
        return ret;
    }

    public PlayerSaveData LoadPlayerData()
    {
        PlayerSaveData data = null;
        if (File.Exists(Application.persistentDataPath + SaveFilePaths.PLAYER_SAVE))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + SaveFilePaths.PLAYER_SAVE,FileMode.Open);
            data = (PlayerSaveData)bf.Deserialize(file);
            file.Close();
        }

        return data;
    }

    public DoubleCashSaveData LoadDoubleCashData()
    {
        DoubleCashSaveData data = null;
        if (File.Exists(Application.persistentDataPath + SaveFilePaths.DOUBLE_CASH_SAVE))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + SaveFilePaths.DOUBLE_CASH_SAVE,FileMode.Open);
            data = (DoubleCashSaveData)bf.Deserialize(file);
            file.Close();
        }
        return data;
    }

    public ManagerSaveData LoadManagerData(string name)
    {
        ManagerSaveData ret = null;
        managersDictionary.TryGetValue(name,out ret);
        return ret;
    }

    private void WriteBusinessDictionary()
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream file = File.Create(Application.persistentDataPath + SaveFilePaths.BUSINESS_SAVE))
        {
            bf.Serialize(file, businessDictionary);
        }

    }

    private void WriteManagersDictionary()
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream file = File.Create(Application.persistentDataPath + SaveFilePaths.MANAGERS_SAVE))
        {
            bf.Serialize(file, managersDictionary);
        }

    }


    public void ReadBusinessDictionary()
    {
        if (File.Exists(Application.persistentDataPath + SaveFilePaths.BUSINESS_SAVE))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + SaveFilePaths.BUSINESS_SAVE,FileMode.Open);
            businessDictionary = (Dictionary<BusinessID,BusinessSaveData>) bf.Deserialize(file);
            file.Close();
        }
    }

    public void ReadManagersDictionary()
    {
        if (File.Exists(Application.persistentDataPath + SaveFilePaths.MANAGERS_SAVE))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + SaveFilePaths.MANAGERS_SAVE, FileMode.Open);
            managersDictionary = (Dictionary<string, ManagerSaveData>)bf.Deserialize(file);
            file.Close();
        }
    }

    public void ReadUpgradesDictionary()
    {
        if (File.Exists(Application.persistentDataPath + SaveFilePaths.UPGRADES_SAVE))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + SaveFilePaths.UPGRADES_SAVE, FileMode.Open);
            upgradesDictionary = (Dictionary<string, UpgradeSaveData>)bf.Deserialize(file);
            file.Close();
        }
    }

    public void ReadQuestsGroupDictionary()
    {
        if (File.Exists(Application.persistentDataPath + SaveFilePaths.QUESTS_SAVE))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + SaveFilePaths.QUESTS_SAVE,FileMode.Open);
            questsGroupDictionary = (Dictionary<string, QuestGroupSaveData>)bf.Deserialize(file);
            file.Close();
        }
    }

    private void OnApplicationQuit()
    {
        //---------Business Save Files------------------

        Debug.Log("SAVING BUSINESS DATA");
        foreach (Transform item in BusinessHolder.Instance.transform)
        {
            Business business = item.GetComponent<Business>();
            if (business)
            {
                BusinessSaveData saveData = business.CreateSaveData();
                SaveBusinessData(business.ID,saveData);
            }
        }

        //writing dictionary now
        WriteBusinessDictionary();
        Debug.Log("SAVING BUSINESS DATA COMPLETE");

        //--------

        //------------Player Save Files---------------------

        Debug.Log("SAVING PLAYER DATA");
        PlayerSaveData data = Player.Instance.CreateSaveData();
        WritePlayerData(data);

        //-------

        //-------------Managers Save File-----------------
        Debug.Log("SAVING MANAGERS DATA");
        foreach (Transform item in ManagerOderer.Instance.transform)
        {
            Manager manager = item.GetComponent<Manager>();
            if (manager)
            {
                ManagerSaveData saveData = manager.CreateSaveData();
                SaveManagerData(manager.Name,saveData);
            }
        }

        //Writing managers dictionary now coz all managers have been added to dictionary
        WriteManagersDictionary();
        Debug.Log("SAVING MANAGERS DATA COMPLETE");

        //---------------Upgrades save file-----------------------
        Debug.Log("SAVING UPGRADES DATA");
        foreach (Transform item in UpgradesHolder.Instance.transform)
        {
            Upgrade upgrade = item.GetComponent<Upgrade>();
            if (upgrade)
            {
                UpgradeSaveData saveData = upgrade.CreateSaveData();
                SaveUpgradesData(upgrade.Id, saveData);
            }
        }
        WriteUpgradesDictionary();
        Debug.Log("SAVING UPGRADES DATA COMPLETE");

        //-----------Quest Save Files---------------------
        Debug.Log("SAVING QUESTS DATA");
        foreach (Transform item in QuestGroupHolder.Instance.transform)
        {
            QuestGroup questGroup = item.GetComponent<QuestGroup>();
            if (questGroup)
            {
                QuestGroupSaveData saveData = questGroup.CreateQuestgroupSaveData();
                SaveQuestGroupData(questGroup.Id,saveData);
            }
        }
        WriteQuestGroupDictionary();
        Debug.Log("SAVING QUESTS DATA COMPLETE");

        //--------------Double cash save files
        Debug.Log("SAVING DOUBLE CASH DATA");
        DoubleCashSaveData doubleCashSaveData = DoubleCashController.Instance.CreateSaveData();
        WriteDoubleCashData(doubleCashSaveData);
        Debug.Log("SAVING DOUBLE CASH DATA COMPLETE");





    }
}
