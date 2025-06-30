using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpPoints : MonoBehaviour
{
    private float scoreToGive=25.0f;
    private Score theScore;

    void Start()
    {
        theScore = FindObjectOfType<Score>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            FindObjectOfType<AudioManager>().PlaySound("PickUp");
            theScore.AddScore(scoreToGive);
            gameObject.SetActive(false);



        }
    }
}
