using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesHolder : MonoBehaviour
{
    static UpgradesHolder instance = null;
    public static UpgradesHolder Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance !=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
