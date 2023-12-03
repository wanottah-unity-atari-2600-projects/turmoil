
using System.Collections.Generic;
using UnityEngine;

//
// Turmoil 1982 v2021.02.06
//
// 2021.02.04
//

public class MissilePooler : MonoBehaviour
{
    public static MissilePooler pooler;

    public GameObject missile;

    public List<GameObject> pooledMissiles;

    private int missilesToPool;


    private void Awake()
    {
        pooler = this;
    }


    private void Start()
    {
        Initialise();
    }


    private void Initialise()
    {
        pooledMissiles = new List<GameObject>();

        missilesToPool = 10;

        for (int i = 0; i < missilesToPool; i++)
        {
            GameObject missileGameObject = Instantiate(missile, transform);

            missileGameObject.SetActive(false);

            pooledMissiles.Add(missileGameObject);
        }
    }


    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledMissiles.Count; i++)
        {
            if (!pooledMissiles[i].activeInHierarchy)
            {
                return pooledMissiles[i];
            }
        }

        return null;
    }


} // end of class
