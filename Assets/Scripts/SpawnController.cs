using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] minions;
    public int spawnNumber = 3;
    public int spawnInterval = 3;
    int timeCounter = 0;

    private void Start()
    {
        StartCoroutine(WaitAndSpawn());
    }

    IEnumerator WaitAndSpawn()
    {

        
        if (timeCounter < spawnNumber)
        {
            var minion = Instantiate(minions[0], transform.position, Quaternion.identity);
            timeCounter++;
            yield return new WaitForSeconds(spawnInterval);
            StartCoroutine(WaitAndSpawn());
        }
    }



}
