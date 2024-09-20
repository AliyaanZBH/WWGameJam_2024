using UnityEngine;
using UnityEngine.UI;  // Required for manipulating UI Text
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Text finalScoreText;  // Reference to the UI Text object where the final score is displayed

    // Start is called before the first frame update
    void Start()
    {
        // Display the score from the previous scene
        finalScoreText.text = "Final Score: " + ScoreManager.score;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Load the "GeorgeJam" scene
            SceneManager.LoadScene("start screen");
        }
    }




}