using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    struct MyGrid
    {
        public MyGrid(int x, int y) { this.x = x; this.y = y; this.cells = new List<MyCell>(); }
        public int x, y;
        public List<MyCell> cells;
    }
    struct MyCell
    {

        public MyCell(GameObject obj, int x, int y) { this.fruit = obj; this.x = x; this.y = y; }

        public GameObject fruit;
        public int x, y;
    }

    private MyGrid grid;

    [SerializeField] private int ROWS = 6;
    [SerializeField] private int COLS = 6;

    [SerializeField] private List<GameObject> selectableFruit = new List<GameObject>();

    // Generate a 6x6 grid of randomised fruit.

    void Pick2x2()
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
        int startX = UnityEngine.Random.Range(0, ROWS - 1);
        int startY = UnityEngine.Random.Range(0, COLS - 1);

        // Collect the 2x2 block of fruit based on the starting point and direction.
        List<GameObject> selectedFruits = new List<GameObject>();

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

    void Start()
    {
        grid = new MyGrid(0,0 );

        for (int x = 0; x < ROWS; x++)
        {
            for (int y = 0; y < COLS; y++)
            {
                // Pick a random fruit.
                GameObject fruit = selectableFruit[UnityEngine.Random.Range(0, selectableFruit.Count)];
                fruit.transform.position = new Vector3(x, y, 0);
                GameObject obj = Instantiate(fruit);

                grid.cells.Add(new MyCell(obj,x,y));

            }
        }

        // Pick a random 2x2 of fruit for the round.
        Pick2x2();
    }
}

