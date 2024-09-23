using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private void Start()
    {
        scoreText.text = "Final score: " + GameScoreManager.score.ToString();

        GameScoreManager.Save();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {


            SceneManager.LoadScene("Title");
        }
    }
}
