using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OnEnableManagerDataSetter : MonoBehaviour
{
    public void OnEnable()
    {
        Manager manager = GetComponent<Manager>();
        if (Application.isEditor && !Application.isPlaying)
        {
            manager.SetData(manager.Name,manager.Description,manager.Price,manager.Sprite);

        }
    }
}
