using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Required to manipulate UI elements like Text and Scene management

public class ScoreManager : MonoBehaviour
{
    // Reference to the Text GameObject
    public Text scoreText;

    // Static variable to store the score between scenes
    public static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        // Initialize score display
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the spacebar is pressed to increase the score
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseScore();
        }

        // Check if the Escape key is pressed to end the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Endgame();
        }
    }

    // Method to increase the score
    void IncreaseScore()
    {
        score++;  // Increase the static score
        UpdateScoreText();
    }

    // Method to update the score text
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;  // Update the score display
    }

    // Method to load the game over scene
    void Endgame()
    {
        // Load the GameoverScene when Escape is pressed
        SceneManager.LoadScene("GameoverScene");

        // Optional: Log a message for debugging
        Debug.Log("Go to game over screen with score: " + score);
    }
}
