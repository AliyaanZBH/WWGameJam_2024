using UnityEngine;
using UnityEngine.UI;  // Required to manipulate UI elements like Text

public class ScoreManager : MonoBehaviour
{
    // Reference to the Text GameObject
    public Text scoreText;

    // Variable to store the score
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize score display
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseScore();
        }
    }

    // Method to increase the score
    void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }

    // Method to update the score text
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}