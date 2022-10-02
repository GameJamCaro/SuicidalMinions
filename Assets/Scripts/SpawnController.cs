using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] minions;
    public int spawnNumber = 3;
    public int spawnInterval = 3;
    int timeCounter = 0;
    public bool randomMinionDir;

    private void Start()
    {
        StartCoroutine(WaitAndSpawn());
    }

    IEnumerator WaitAndSpawn()
    {

        
        if (timeCounter < spawnNumber)
        {
            var minion = Instantiate(minions[0], transform.position, Quaternion.identity);
            if (randomMinionDir)
            {
                minion.GetComponent<MinionController>().RandomizeDir();
            }
            timeCounter++;
            yield return new WaitForSeconds(Random.Range(spawnInterval-1, spawnInterval+1));
            StartCoroutine(WaitAndSpawn());
        }
    }



}
