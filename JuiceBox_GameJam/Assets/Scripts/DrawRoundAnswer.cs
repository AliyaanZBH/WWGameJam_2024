using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions;
using static GridGenerator;

public class DrawRoundAnswer : MonoBehaviour
{
    [SerializeField] private GameObject gridObject;
    private GridGenerator gridClass;
    private MyGrid grid;

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;


    [SerializeField] private List<AudioClip> sounds;
    private AudioSource aux;

    [SerializeField] private float difficultyMultiplier = 0.9f;
    private float baseTime = 7;

    private bool player1Correct = false, player2Correct = false;

    // What makes up an answer? We need a cell with an objects and a name
    public struct AnswerCell
    {
        public GameObject obj;
        public EMyFruit name;

        public AnswerCell(GameObject gObj, EMyFruit gName) { obj = gObj; name = gName; }
    }

    // Where to start drawing the answer.
    [SerializeField] private GameObject drawPoint;

    // An answer is simply a list of cells
    public List<AnswerCell> displayedAnswer = new List<AnswerCell>();
    public List<AnswerCell> selectedAnswer = new List<AnswerCell>();

    public List<EMyFruit> player1Answer = new List<EMyFruit>();
    public List<EMyFruit> player2Answer = new List<EMyFruit>();

    private bool bNewRound = true;

    private void Start()
    {
        // Grab our grid
        Assert.IsNotNull(gridObject);
        gridClass = gridObject.GetComponent<GridGenerator>();
        grid = gridClass.GetGrid();

        aux = GetComponent<AudioSource>();
    }


    void Pick2x2(ref List<AnswerCell> selectedAnswer)
    {

        // Pick a random fruit within the internal 5x5 (of a 6x6 grid)
        // Pick a random direction to create a square
        // Pick the other fruits to make the 2x2 
        // For example
        // x x x x
        // x x x x
        // x x x x
        // x x x x
        // A random fruit from the central 2x2 is picked 
        // x x x x
        // x x x x
        // x x o x
        // x x x x
        // One random direction is picked to fill the 2x2, these are the potential options
        // x x x x
        // x x o x
        // x o o o
        // x x o x
        // Fill in the 2x2 from that direction
        // x x x x
        // x x o o
        // x x o o
        // x x x x

        // Pick a random fruit within the internal 5x5 grid.
        int startX = UnityEngine.Random.Range(0, grid.x - 1);
        int startY = UnityEngine.Random.Range(0, grid.y - 1);

        // Collect the 2x2 block of fruit based on the starting point and direction.
        //List<GameObject> selectedAnswer = new List<GameObject>();

        // Add the starting fruit.

        // Get the fruit from the list of cells from the start x and y.

        // Bottom left
        for (int i = 0; i < grid.cells.Count; i++)
        {
            if (grid.cells[i].x == startX && grid.cells[i].y == startY)
            {
                Debug.Log("First fruit: " + grid.cells[i].fruit.name + " " + grid.cells[i].x + ", " + grid.cells[i].y);
                
                selectedAnswer.Add(new AnswerCell(grid.cells[i].fruit, grid.cells[i].name));
            }
        }

        // top right
        for (int i = 0; i < grid.cells.Count; i++)
        {
            if (grid.cells[i].x == startX + 1 && grid.cells[i].y == startY + 1)
            {
                Debug.Log("Second fruit: " + grid.cells[i].fruit.name + " " + grid.cells[i].x + ", " + grid.cells[i].y);
                selectedAnswer.Add(new AnswerCell(grid.cells[i].fruit, grid.cells[i].name));
            }
        }

        // top left
        for (int i = 0; i < grid.cells.Count; i++)
        {
            if (grid.cells[i].x == startX && grid.cells[i].y == startY + 1)
            {
                Debug.Log("Third fruit: " + grid.cells[i].fruit.name + " " + grid.cells[i].x + ", " + grid.cells[i].y);
                selectedAnswer.Add(new AnswerCell(grid.cells[i].fruit, grid.cells[i].name));
            }
        }

        // bottom right 
        for (int i = 0; i < grid.cells.Count; i++)
        {
            if (grid.cells[i].x == startX + 1 && grid.cells[i].y == startY)
            {
                Debug.Log("Last fruit: " + grid.cells[i].fruit.name + " " + grid.cells[i].x + ", " + grid.cells[i].y);
                selectedAnswer.Add(new AnswerCell(grid.cells[i].fruit, grid.cells[i].name));
            }
        }
    }

    private void Update()
    {
        Puzzle_Controller player1Controller = player1.GetComponent<Puzzle_Controller>();
        Puzzle_Controller player2Controller = player2.GetComponent<Puzzle_Controller>();

        player1Answer = player1Controller.GetAnswer();
        player2Answer = player2Controller.GetAnswer();


        // Create an answer that the players have to find
        // Pick a random 2x2 of fruit for the round.

        if (bNewRound)
        {

            // Check that the grid is valid
            if (grid.cells != null && grid.cells.Count != 0)
            {
                Pick2x2(ref selectedAnswer);

                // Draw the new fruit in the answer location, based on the fruit we selected from the grid
                for (int j = 0; j < selectedAnswer.Count; j++)
                {
                    GameObject fruit = Instantiate(selectedAnswer[j].obj);

                    // Wants to be in this order coz the fruits in selectedAnswer were added this way.
                    // 1. bottom left / 2. top right / 3. top left / 4. bottom right
                    if (j == 0) fruit.transform.position = new Vector3(drawPoint.transform.position.x, drawPoint.transform.position.y, 0);               // First fruit 
                    else if (j == 1) fruit.transform.position = new Vector3(drawPoint.transform.position.x + 1, drawPoint.transform.position.y + 1, 0);  // Second fruit 
                    else if (j == 2) fruit.transform.position = new Vector3(drawPoint.transform.position.x, drawPoint.transform.position.y + 1, 0);      // Third fruit 
                    else if (j == 3) fruit.transform.position = new Vector3(drawPoint.transform.position.x + 1, drawPoint.transform.position.y, 0);      // Last fruit 

                    displayedAnswer.Add(new AnswerCell(fruit, selectedAnswer[j].name));
                }

                // Reset flag until the next round
                bNewRound = false;
            }
        }

        if (player1Answer.Count != 0 && player2Answer.Count != 0)
        {

            // bottom left, top left
            if (player1Answer[0] == displayedAnswer[0].name && player1Answer[1] == displayedAnswer[2].name)
            {
                Debug.Log("Player1 is correct\n");
                player1Correct = true;
            }


            // bottom right, top right
            if (player2Answer[0]== displayedAnswer[3].name && player2Answer[1]== displayedAnswer[1].name)
            {
                Debug.Log("Player2 is correct\n");
                player2Correct = true;
            }

            if (player1Correct && player2Correct)
            {
                Debug.Log("Win round\n");
                gridClass.ResetGrid();
                gridClass.GenerateGrid();

                // Reset values
                ResetAnswer();

                // Reset flags
                player1Correct = false;
                player2Correct = false;
                bNewRound = true;

                baseTime *= difficultyMultiplier;
                RoundTimer.timer = 3 + baseTime;

                GameScoreManager.IncreaseScore();

                // Play sound
                aux.PlayOneShot(sounds[0]);

                // Reset input lock
                player1Controller.UnlockAnswer();
                player2Controller.UnlockAnswer();


            }
        }
    }

    public void ResetAnswer()
    {
        Debug.Log("Clear Answer");

        // Reset previous answer
        for (int i = 0; i < selectedAnswer.Count; i++)
        {
            Destroy(selectedAnswer[i].obj);
            Destroy(displayedAnswer[i].obj);
        }
        selectedAnswer.Clear();
        displayedAnswer.Clear();

        // Clear player submitted answers
        player1Answer.Clear();
        player2Answer.Clear();


    }
}
