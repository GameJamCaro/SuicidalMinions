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
    public int totalMinions = 10;
    MinionController randomMinion;
    int leavingMinions;
    public GameObject endPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        StartCoroutine(TenSecondsCountDown());
        minions = GameObject.FindGameObjectsWithTag("Minion");

        DisplayScore();
    }

    public void AddToScore()
    {
        score++;
        AddLeavingtMinion();
        DisplayScore();
        
    }


    public void AddLeavingtMinion()
    {
        if (leavingMinions < totalMinions-1)
        {
            leavingMinions++;
        }
        else
        {
            StartCoroutine(LevelEnd());
        }
        Debug.Log("leaving minions: " + leavingMinions);
    }

    private IEnumerator LevelEnd()
    {
        yield return new WaitForSeconds(1);
        endPanel.SetActive(true);
        Time.timeScale = 0;
    }

    void DisplayScore()
    {
        scoreUI.text = "Saved: " + score + " / " + totalMinions;
    }

    IEnumerator TenSecondsCountDown()
    {
        yield return new WaitForSeconds(2);
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
        if (minions.Length > 0)
        {
            GetRandomMinion();
            randomMinion.Discomfort();
        }
    }

    void GetRandomMinion()
    {
        int random = Random.Range(0, minions.Length);
        if (minions.Length > 0)
        {
            if (minions[random] != null)
            {
                randomMinion = minions[random].GetComponent<MinionController>();
            }
            else
            {
                Debug.Log(random);
                
            }
        }
        
    }

}
