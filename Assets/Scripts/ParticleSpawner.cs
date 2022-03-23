using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GIKIT;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] GameObject particlePrefab = null;
    [SerializeField] Transform positionTransform = null;
    [SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] float lifeTime = 3f;
    [SerializeField] AudioClip particleSound = null;

    public void Spawn()
    {
        if (particlePrefab)
        {
            GameObject spawnedParticles =  Instantiate(particlePrefab,positionTransform.position + offset,Quaternion.identity);
            Destroy(spawnedParticles, lifeTime);
            MusicManager.Instance.PlaySFX(particleSound);
        }
    }
}
