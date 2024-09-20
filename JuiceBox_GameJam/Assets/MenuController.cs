using UnityEngine;
using UnityEngine.SceneManagement;  // Needed to switch scenes

public class MenuController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Load the "GeorgeJam" scene
            SceneManager.LoadScene("GeorgeJam");
        }
    }
}

