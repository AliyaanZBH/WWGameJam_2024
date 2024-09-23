using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundTimer : MonoBehaviour
{
    public static float timer = 10;
    [SerializeField] public Text timerText;
    void Update()
    {
        if (timer > 0) //time above 0
        {
            timer -= Time.deltaTime; // reduce time
            timerText.text = Mathf.Round(timer).ToString(); 
        }

        else if (timer <= 0) //time at zero, game over
        {
            // add function to end game or crash it idk, prob just stop movement and add text on screen
            SceneManager.LoadScene("GameOver");
        }

    }
}
