using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static GridGenerator;

public class DrawRoundAnswer : MonoBehaviour
{
    [SerializeField] private GameObject grid;
    [SerializeField] private GridGenerator gridGen;

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    private bool player1Correct = false, player2Correct = false;


    public struct Answer
    {
        private GameObject obj;
        public string name;
    }

    public List<GameObject> previousAnswer = new List<GameObject>();
    public List<GameObject> selectedFruits = new List<GameObject>();




    private void Start()
    {
        gridGen = grid.GetComponent<GridGenerator>();
    }


    void Pick2x2(ref List<GameObject> selectedFruits)
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
        int startX = UnityEngine.Random.Range(0, gridGen.GetGrid().x - 1);
        int startY = UnityEngine.Random.Range(0, gridGen.GetGrid().y - 1);

        // Collect the 2x2 block of fruit based on the starting point and direction.
        //List<GameObject> selectedFruits = new List<GameObject>();

        // Add the starting fruit.

        // Get the fruit from the list of cells from the start x and y.

        // Bottom left
        for (int i = 0; i < gridGen.GetGrid().cells.Count; i++)
        {
            if (gridGen.GetGrid().cells[i].x == startX && gridGen.GetGrid().cells[i].y == startY)
            {
                Debug.Log("First fruit: " + gridGen.GetGrid().cells[i].fruit.name + " " + gridGen.GetGrid().cells[i].x + ", " + gridGen.GetGrid().cells[i].y);
                selectedFruits.Add(gridGen.GetGrid().cells[i].fruit);
            }
        }

        // top right
        for (int i = 0; i < gridGen.GetGrid().cells.Count; i++)
        {
            if (gridGen.GetGrid().cells[i].x == startX + 1 && gridGen.GetGrid().cells[i].y == startY + 1)
            {
                Debug.Log("Second fruit: " + gridGen.GetGrid().cells[i].fruit.name + " " + gridGen.GetGrid().cells[i].x + ", " + gridGen.GetGrid().cells[i].y);
                selectedFruits.Add(gridGen.GetGrid().cells[i].fruit);
            }
        }

        // top left
        for (int i = 0; i < gridGen.GetGrid().cells.Count; i++)
        {
            if (gridGen.GetGrid().cells[i].x == startX && gridGen.GetGrid().cells[i].y == startY + 1)
            {
                Debug.Log("Third fruit: " + gridGen.GetGrid().cells[i].fruit.name + " " + gridGen.GetGrid().cells[i].x + ", " + gridGen.GetGrid().cells[i].y);
                selectedFruits.Add(gridGen.GetGrid().cells[i].fruit);
            }
        }

        // bottom right 
        for (int i = 0; i < gridGen.GetGrid().cells.Count; i++)
        {
            if (gridGen.GetGrid().cells[i].x == startX + 1 && gridGen.GetGrid().cells[i].y == startY)
            {
                Debug.Log("Last fruit: " + gridGen.GetGrid().cells[i].fruit.name + " " + gridGen.GetGrid().cells[i].x + ", " + gridGen.GetGrid().cells[i].y);
                selectedFruits.Add(gridGen.GetGrid().cells[i].fruit);
            }
        }
    }


    private void Update()
    {
        Puzzle_Controller player1Controller = player1.GetComponent<Puzzle_Controller>();
        Puzzle_Controller player2Controller = player2.GetComponent<Puzzle_Controller>();

        List<GameObject> player1Answer = player1Controller.GetAnswer();
        List<GameObject> player2Answer = player2Controller.GetAnswer();


        // Create an answer that the players have to find
        // Pick a random 2x2 of fruit for the round.
        Pick2x2(ref selectedFruits);

        for (int j = 0; j < selectedFruits.Count; j++)
        {
            GameObject fruit = new GameObject();
            fruit.AddComponent<SpriteRenderer>();
            fruit.GetComponent<SpriteRenderer>().sprite = selectedFruits[j].GetComponent<SpriteRenderer>().sprite;

            // Wants to be in this order coz the fruits in selectedFruits were added this way.
            // 1. bottom left / 2. top right / 3. top left / 4. bottom right
            if (j == 0) fruit.transform.position = new Vector3(drawPoint.transform.position.x, drawPoint.transform.position.y, 0);               // First fruit 
            else if (j == 1) fruit.transform.position = new Vector3(drawPoint.transform.position.x + 1, drawPoint.transform.position.y + 1, 0);  // Second fruit 
            else if (j == 2) fruit.transform.position = new Vector3(drawPoint.transform.position.x, drawPoint.transform.position.y + 1, 0);      // Third fruit 
            else if (j == 3) fruit.transform.position = new Vector3(drawPoint.transform.position.x + 1, drawPoint.transform.position.y, 0);      // Last fruit 

            fruit.name = "THIS IS A DEBUG FRUIT" + selectedFruits[j].name;
            previousAnswer.Add(fruit);
        }


        // in order bottom left, top right, top left bottom right
        List<GameObject> answer = gridGen.previousAnswer;

        if (player1Answer.Count != 0 && player2Answer.Count != 0)
        {

            // bottom left, top left
            if (player1Answer[0].GetComponent<SpriteRenderer>().sprite == answer[0].GetComponent<SpriteRenderer>().sprite && player1Answer[1].GetComponent<SpriteRenderer>().sprite == answer[2].GetComponent<SpriteRenderer>().sprite)
            {
                Debug.Log("Player1 is correct\n");
                player1Correct = true;
            }

            // bottom right, top right
            if (player2Answer[0].GetComponent<SpriteRenderer>().sprite == answer[3].GetComponent<SpriteRenderer>().sprite && player2Answer[1].GetComponent<SpriteRenderer>().sprite == answer[1].GetComponent<SpriteRenderer>().sprite)
            {
                Debug.Log("Player2 is correct\n");
                player2Correct = true;
            }

            if (player1Correct && player2Correct)
            {
                Debug.Log("Win round\n");
                gridGen.ResetGrid();
                gridGen.GenerateGrid();

                answer.Clear();
                player1Answer.Clear();
                player2Answer.Clear();
                player1Correct = false;
                player2Correct = false;

            }
        }
    }
}
