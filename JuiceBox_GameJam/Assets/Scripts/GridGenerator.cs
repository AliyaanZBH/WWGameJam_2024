using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    struct MyGrid
    {
        public MyGrid(int x, int y) { this.x = x; this.y = y; this.fruit = new GameObject(); }
        public int x, y;
        public GameObject fruit;
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

        // Define the possible directions to complete the 2x2 square.
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),  // Right
            new Vector2Int(1, 0),  // Down
            new Vector2Int(1, 1),  // Diagonal Down-Right
            new Vector2Int(1, -1)  // Diagonal Down-Left
        };

        // Pick a random direction.
        Vector2Int direction = directions[UnityEngine.Random.Range(0, directions.Length)];

        // Collect the 2x2 block of fruit based on the starting point and direction.
        List<GameObject> selectedFruits = new List<GameObject>();

        // Add the starting fruit.
        //grid = new MyGrid(startX, startY);
        grid.x = startX;
        grid.y = startY;
        selectedFruits.Add(grid.fruit);

        // Add the other fruits to form the 2x2 block.
        grid.x = startX + direction.x;
        grid.y = startY+ direction.y;
        selectedFruits.Add(grid.fruit);

        grid.x = startX + 1;
        grid.y = startY;
        selectedFruits.Add(grid.fruit);

        grid.x = startX + direction.x;
        grid.y = startY + direction.y;
        selectedFruits.Add(grid.fruit);

        // Log the selected 2x2 fruits.
        Debug.Log("Random 2x2 fruit selected for this round:");
        foreach (GameObject _fruit in selectedFruits)
        {
            Debug.Log(_fruit.name);
        }
    }

    void Start()
    {
        for (int x = 0; x < ROWS; x++)
        {
            for (int y = 0; y < COLS; y++)
            {
                // Pick a random fruit.
                GameObject fruit = selectableFruit[UnityEngine.Random.Range(0, selectableFruit.Count)];
                fruit.transform.position = new Vector3(x, y, 0);
                GameObject obj = Instantiate(fruit);

                grid.fruit = obj;
                grid.x = x;
                grid.y = y;
            }
        }

        // Pick a random 2x2 of fruit for the round.
        Pick2x2();
    }
}

