using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ManagerAction : ScriptableObject
{
    public abstract void Act(Business business);
}
