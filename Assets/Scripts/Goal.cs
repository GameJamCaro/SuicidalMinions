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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        score.AddToScore();
        Destroy(collision.gameObject);
    }




}
