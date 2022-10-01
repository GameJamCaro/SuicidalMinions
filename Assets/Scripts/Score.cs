using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreUI;
    int countDownInt = 10;
    GameObject[] minions;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        StartCoroutine(TenSecondsCountDown());
    }

    public void AddToScore()
    {
        score++;
        scoreUI.text = "Rescued: " + score + " / 3";
    }

    IEnumerator TenSecondsCountDown()
    {
        yield return new WaitForSeconds(1);
        if (countDownInt > 0)
        {
            countDownInt--;
        }
        else
        {
            Discomfort();
            countDownInt = 10;
        }
        StartCoroutine(TenSecondsCountDown());
    }

    void Discomfort()
    {
        minions = GameObject.FindGameObjectsWithTag("Minion");
        GetRandomMinion().Discomfort();
    }

    MinionController GetRandomMinion()
    {
        return minions[Random.Range(0,minions.Length-1)].GetComponent<MinionController>();
    }

}
