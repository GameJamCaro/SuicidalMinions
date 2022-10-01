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
        scoreUI.text = "Rescued: " + score + " / 6";
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
        if (minions != null)
        {
            GetRandomMinion().Discomfort();
        }
    }

    MinionController GetRandomMinion()
    {
        int random = Random.Range(0, minions.Length);
        if (minions != null)
        {
            if (minions[random] != null)
            {
                return minions[random].GetComponent<MinionController>();
            }
            else
            {
                Debug.Log(random);
                return GetRandomMinion();
            }
        }
        else
        {
            Debug.Log(random);
            return null;
        }
    }

}
