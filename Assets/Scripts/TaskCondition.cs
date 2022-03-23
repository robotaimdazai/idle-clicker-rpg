
using UnityEngine;

public abstract class TaskCondition : ScriptableObject
{
    public abstract void Init(Task task);
    public abstract bool Check(Task task);
}
