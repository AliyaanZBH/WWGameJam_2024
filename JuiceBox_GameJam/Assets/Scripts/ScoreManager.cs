using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Reference to the Text GameObject
    public static Text scoreText;

    // Static variable to store the score between scenes
    public static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;

        scoreText = GetComponentInChildren<Text>();

        // Initialize score display
        scoreText.text = score.ToString();  // Update the score display

    }

    // Method to increase the score
    public static void IncreaseScore()
    {
        score++;  // Increase the static score
        scoreText.text = score.ToString();  // Update the score display
    }
}
