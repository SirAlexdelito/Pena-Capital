using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject persistentObject;
    private static bool hasSpawned = false;

    private void Awake()
    {
        if (hasSpawned)
        {
            return;
        }
        Spawn();
        hasSpawned = true;

    }
    void Spawn()
    {
        GameObject pO = Instantiate(persistentObject);
        persistentObject.SetActive(true);
        DontDestroyOnLoad(pO);
    }
}
