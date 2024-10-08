using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundTimer : MonoBehaviour
{
    public static float timer = 30;
    [SerializeField] public Text timerText;

    public static float baseTime = 30;

    private AudioSource aux;


    private void Start()
    {
        timer = baseTime;
        aux = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (timer > 0) //time above 0
        {
            timer -= Time.deltaTime; // reduce time
            timerText.text = Mathf.Round(timer).ToString(); 
        }

        else if (timer <= 0) //time at zero, game over
        {
            // Play Gameover sound and wait
            SceneManager.LoadScene("GameOver");

            //StartCoroutine(WaitForSoundAndGameover(aux.clip));

        }

    }

    private IEnumerator WaitForSoundAndGameover(AudioClip clip)
    {
        aux.Play();
        yield return new WaitUntil(() => aux.time >= clip.length);
        // Load new scene after sound
        SceneManager.LoadScene("GameOver");

    }
}
