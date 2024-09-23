using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{


    [SerializeField] private float timer = 10;
    [SerializeField] public Text timerText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (timer > 0) //time above 0
        {
            timer = timer - Time.deltaTime; // reduce time

            Debug.Log("Timer is at" + Mathf.Round(timer)); // display time in log

            timerText.text = ("Time: " + Mathf.Round(timer)); //display time to user
        }

        else if (timer <= 0) //time at zero, game over
        {
            Debug.Log("game over");
            // add function to end game or crash it idk, prob just stop movement and add text on screen
        }

    }




}
