using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Score score;


    private void OnTriggerEnter(Collider other)
    {
        score.AddToScore();
        Destroy(other.gameObject);
    }

   



}
