using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct MyGrid
    {
        public MyGrid(int x, int y) { this.x = x; this.y = y; this.cells = new List<MyCell>(); }
        public int x, y;
        public List<MyCell> cells;
    }
    public struct MyCell
    {

        public MyCell(GameObject obj, int x, int y) { this.fruit = obj; this.x = x; this.y = y; }

        public GameObject fruit;
        public int x, y;
    }

    struct Answer
    {
        MyGrid answerGrid;
    }




    [SerializeField] private GameObject drawPoint;

    private bool bReset = false;

    [SerializeField] private MyGrid grid;

    [SerializeField] private List<GameObject> selectableFruit = new List<GameObject>();


    // Public accessor to retrieve grid for player
    public MyGrid GetGrid() { return grid; }

    // Generate a 6x6 grid of randomised fruit.

    public List<GameObject> previousAnswer = new List<GameObject>();
    public List<GameObject> selectedFruits = new List<GameObject>();

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
        int startX = UnityEngine.Random.Range(0, grid.x - 1);
        int startY = UnityEngine.Random.Range(0, grid.y - 1);

        // Collect the 2x2 block of fruit based on the starting point and direction.
        //List<GameObject> selectedFruits = new List<GameObject>();

        // Add the starting fruit.

        // Get the fruit from the list of cells from the start x and y.

        // Bottom left
        for (int i = 0; i < grid.cells.Count; i++)
        {
            if (grid.cells[i].x == startX && grid.cells[i].y == startY)
            {
                Debug.Log("First fruit: " + grid.cells[i].fruit.name + " " + grid.cells[i].x + ", " + grid.cells[i].y);
                selectedFruits.Add(grid.cells[i].fruit);
            }
        }

        // top right
        for (int i = 0; i < grid.cells.Count; i++)
        {
            if (grid.cells[i].x == startX + 1 && grid.cells[i].y == startY + 1)
            {
                Debug.Log("Second fruit: " + grid.cells[i].fruit.name + " " + grid.cells[i].x + ", " + grid.cells[i].y);
                selectedFruits.Add(grid.cells[i].fruit);
            }
        }

        // top left
        for (int i = 0; i < grid.cells.Count; i++)
        {
            if (grid.cells[i].x == startX && grid.cells[i].y == startY + 1)
            {
                Debug.Log("Third fruit: " + grid.cells[i].fruit.name + " " + grid.cells[i].x + ", " + grid.cells[i].y);
                selectedFruits.Add(grid.cells[i].fruit);
            }
        }

        // bottom right 
        for (int i = 0; i < grid.cells.Count; i++)
        {
            if (grid.cells[i].x == startX + 1 && grid.cells[i].y == startY)
            {
                Debug.Log("Last fruit: " + grid.cells[i].fruit.name + " " + grid.cells[i].x + ", " + grid.cells[i].y);
                selectedFruits.Add(grid.cells[i].fruit);
            }
        }
    }

    public void GenerateGrid()
    {
        grid = new MyGrid(6,6 );

        for (int x = 0; x < grid.x; x++)
        {
            for (int y = 0; y < grid.y; y++)
            {
                // Pick a random fruit.
                GameObject fruit = selectableFruit[UnityEngine.Random.Range(0, selectableFruit.Count)];
                fruit.transform.position = new Vector3(x, y, 0);
                GameObject obj = Instantiate(fruit);

                grid.cells.Add(new MyCell(obj, x, y));

            }
        }


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
    }

    void ResetGrid()
    {

        Debug.Log("Clear");


        // Reset previous answer
        for (int i = 0; i < selectedFruits.Count; i++)
        {
            Destroy(selectedFruits[i]);
            Destroy(previousAnswer[i]);
        }
        selectedFruits.Clear();
        previousAnswer.Clear();

        for (int i = 0; i < grid.cells.Count; i++)
        {
            Destroy(grid.cells[i].fruit);
        }

        grid.cells.Clear();
        grid = new MyGrid(0, 0);
    }


    void Start()
    {
        GenerateGrid();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetGrid();

            GenerateGrid();
        }
    }
}

