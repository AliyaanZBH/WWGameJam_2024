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

    [SerializeField] private GameObject drawPoint;

    [SerializeField] private MyGrid grid;

    [SerializeField] private List<GameObject> selectableFruit = new List<GameObject>();


    // Public accessor to retrieve grid for player
    public MyGrid GetGrid() { return grid; }


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
    }

   public void ResetGrid()
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
    }


    void Start()
    {
        GenerateGrid();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ResetGrid();

        //    GenerateGrid();
        //}
    }
}

