using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGroupHolder : MonoBehaviour
{
    private static QuestGroupHolder instance = null;

    public static QuestGroupHolder Instance
    {
        get { return instance; }
    }

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
}
