using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    [SerializeField] private int ROWS = 6;
    [SerializeField] private int COLS = 6;

    [SerializeField] private List<GameObject> selectableFruit = new List<GameObject>();

    // Generate a 6x6 grid of randomised fruit.

    void Start()
    {
        if (selectableFruit.Count > ROWS)
        {
            Debug.Log("Too many fruit");
        }

        for (int x = 0; x < ROWS; x++)
        {
            for (int y = 0; y < COLS; y++)
            {
                // Pick a random fruit.
                GameObject fruit = selectableFruit[UnityEngine.Random.Range(0, selectableFruit.Count)];
                fruit.transform.position = new Vector3(x, y, 0);
                Instantiate(fruit);
            }
        }
    }
}