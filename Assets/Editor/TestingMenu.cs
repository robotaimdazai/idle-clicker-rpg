using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TestingMenu 
{
    [MenuItem("Testing/DeleteTimeStamp")]
    public static void DeleteTimeStamp()
    {
        string LAST_SAVE_DATA_TIME = "LAST_SAVE_DATE_TIME";
        PlayerPrefs.DeleteKey(LAST_SAVE_DATA_TIME);
        Debug.Log("TimeStamp Deleted");
    }

    [MenuItem("Testing/DeleteBusinessSave")]
    public static void DeleteBusinessSave()
    {
        if (File.Exists(Application.persistentDataPath + SaveFilePaths.BUSINESS_SAVE))
        {
            File.Delete(Application.persistentDataPath + SaveFilePaths.BUSINESS_SAVE);
            Debug.Log("Business Data Deleted");
        }
    }

    [MenuItem("Testing/DeletePlayerSave")]
    public static void DeletePlayerSave()
    {
        if (File.Exists(Application.persistentDataPath + SaveFilePaths.PLAYER_SAVE))
        {
            File.Delete(Application.persistentDataPath + SaveFilePaths.PLAYER_SAVE);
            Debug.Log("Player Data Deleted");
        }
    }

    [MenuItem("Testing/DeleteManagerSave")]
    public static void DeleteManagerSave()
    {
        if (File.Exists(Application.persistentDataPath + SaveFilePaths.MANAGERS_SAVE))
        {
            File.Delete(Application.persistentDataPath + SaveFilePaths.MANAGERS_SAVE);
            Debug.Log("Manager Data Deleted");
        }
    }

    [MenuItem("Testing/DeleteUpgradeSave")]
    public static void DeleteUpgradeSave()
    {
        if (File.Exists(Application.persistentDataPath + SaveFilePaths.UPGRADES_SAVE))
        {
            File.Delete(Application.persistentDataPath + SaveFilePaths.UPGRADES_SAVE);
            Debug.Log("Upgrades Data Deleted");
        }
    }
    [MenuItem("Testing/DeleteQuestSave")]
    public static void DeleteQuestSave()
    {
        if (File.Exists(Application.persistentDataPath + SaveFilePaths.QUESTS_SAVE))
        {
            File.Delete(Application.persistentDataPath + SaveFilePaths.QUESTS_SAVE);
            Debug.Log("Quest Data Deleted");
        }
    }

    [MenuItem("Testing/DeleteDoubleCashSave")]
    public static void DeleteDoubleCashSave()
    {
        if (File.Exists(Application.persistentDataPath + SaveFilePaths.DOUBLE_CASH_SAVE))
        {
            File.Delete(Application.persistentDataPath + SaveFilePaths.DOUBLE_CASH_SAVE);
            Debug.Log("Double Cash Data Deleted");
        }
    }

    [MenuItem("Testing/Delete All")]
    public static void DeleteAll()
    {
        DeleteTimeStamp();
        DeleteBusinessSave();
        DeletePlayerSave();
        DeleteManagerSave();
        DeleteUpgradeSave();
        DeleteQuestSave();
    }

}
