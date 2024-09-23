using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private void Start()
    {
        scoreText.text = "Final score: " + ScoreManager.score.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {


            SceneManager.LoadScene("Title");
        }
    }
}
