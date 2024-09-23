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

    [System.Serializable]
    public enum EMyFruit
    {
        STRAWBERRY = 0,
        BANANA = 1,
        GRAPE = 2,
        GRAPEFRUIT = 3,
        BLUEBERRY = 4,
        WATERMELON = 5,
        FRUIT_COUNT = 6
    }

    public struct MyCell
    {

        public MyCell(GameObject obj, int x, int y, EMyFruit name) { this.fruit = obj; this.x = x; this.y = y; this.name = name; }

        public GameObject fruit;
        public EMyFruit name;
        public int x, y;
    }

    [SerializeField] private MyGrid grid;

    [SerializeField] private List<GameObject> selectableFruit = new List<GameObject>();


    // Public accessor to retrieve grid for player
    public MyGrid GetGrid() { return grid; }


    public void GenerateGrid()
    {
        for (int x = 0; x < grid.x; x++)
        {
            for (int y = 0; y < grid.y; y++)
            {
                // Pick a random fruit.
                GameObject fruit = selectableFruit[UnityEngine.Random.Range(0, selectableFruit.Count)];
                // Work out which fruit it is so that we can work out an answer later
                EMyFruit frName = 0;
                switch (fruit.name)
                {
                    case "Strawberry":
                    {
                        frName = EMyFruit.STRAWBERRY;
                        break; 
                    }     
                    
                    case "Blueberry":
                    {
                        frName = EMyFruit.BLUEBERRY;
                        break; 
                    }

                    case "Banana":
                    {
                        frName = EMyFruit.BANANA;
                        break;
                    }   
                    
                    case "Grape":
                    {
                        frName = EMyFruit.GRAPE;
                        break;
                    }   
                    
                    case "Grapefruit":
                    {
                        frName = EMyFruit.GRAPEFRUIT;
                        break;
                    }    
                    
                    case "Watermelon":
                    {
                        frName = EMyFruit.WATERMELON;
                        break;
                    }
                }

                fruit.transform.position = new Vector3(x, y, 0);
                GameObject obj = Instantiate(fruit);

                grid.cells.Add(new MyCell(obj, x, y, frName));

            }
        }
    }

   public void ResetGrid()
    {

        Debug.Log("Clear Grid");


        for (int i = 0; i < grid.cells.Count; i++)
        {
            Destroy(grid.cells[i].fruit);
        }

        grid.cells.Clear();
    }


    void Start()
    {
        grid = new MyGrid(6, 6);
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

