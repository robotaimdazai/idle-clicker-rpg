using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionVFXBehaviour : MonoBehaviour
{
    [Header("Settings")]
    public float SawLifeTime = 2.5f;
    public float HammerLifeTime = 2.5f;

    [Header("Tools Prefab")]
    [SerializeField] GameObject saw;
    [SerializeField] GameObject hammer;

    private void Start()
    {
        Destroy(saw, SawLifeTime);
        Destroy(hammer,HammerLifeTime);
    }
}
