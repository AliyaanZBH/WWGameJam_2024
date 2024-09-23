using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HighscoreTitle : MonoBehaviour
{
    public Text highscoreT;

    void Start()
    {
        int highscore = PlayerPrefs.GetInt("highscore");
        highscoreT.text = "Highscore: " + highscore.ToString();
    }
}
