using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;


    [Range(0f, 1f)]
    public float itemSpawnChance = 0.746f;
    public GameObject[] spawnableItems;
    private GameObject spawnItemTimer;
    private SpawnItemTimer timerScript;

    // Create a new array with the same length as spawnableItems
    GameObject[] regularItems = new GameObject[5];
    GameObject[] specialItems = new GameObject[3];


    public void Start()
    {
        Destroy(gameObject, destructionTime);
        spawnItemTimer = GameObject.Find("SpawnItemTimer");
        timerScript = spawnItemTimer.GetComponent<SpawnItemTimer>();
        Array.Copy(spawnableItems, 0, regularItems, 0, 5); // Copy the first 5 items
        if (timerScript.timer == true)
        {
            Array.Copy(spawnableItems, 5, specialItems, 0, 3); // Copy the 7th and 8th items
            regularItems = regularItems.Concat(specialItems).ToArray();
        }
    }

    private void SpawnItem(float spawnChance, GameObject[] items)
    {
        if (items.Length > 0 && UnityEngine.Random.value < spawnChance)
        {
            int randomIndex = UnityEngine.Random.Range(0, items.Length);
            Instantiate(items[randomIndex], transform.position, Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        SpawnItem(itemSpawnChance, regularItems);
    }

}
