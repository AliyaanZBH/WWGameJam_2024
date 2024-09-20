using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Controller : MonoBehaviour
{

    // How far to move along the grid
    [SerializeField] private float movementDistance = 1f;

    [SerializeField] private int player = 1;

    private Transform trans;
    private Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player == 1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                newPos = new Vector3(0, movementDistance, 0);
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                newPos = new Vector3(0, -movementDistance, 0);
            }  
            
            else if (Input.GetKeyDown(KeyCode.A))
            {
                newPos = new Vector3(-movementDistance, 0, 0);
            }

            else if (Input.GetKeyDown(KeyCode.D))
            {
                newPos = new Vector3(movementDistance, 0, 0);
            }

            // 0 out movement
            else
            {
                newPos = new Vector3(0, 0, 0);

            }
        }
        
        // Repeat for player 2
        // TODO: out of date and wrong
        if (player == 2)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                newPos = new Vector3(0, movementDistance, 0);
            }

            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                newPos = new Vector3(0, -movementDistance, 0);
            }

            else if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                newPos = new Vector3(-movementDistance, 0, 0);
            }

            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                newPos = new Vector3(movementDistance, 0, 0);
            }

            else
            {
                newPos = new Vector3(0, 0, 0);
            }
        }

        trans.position += newPos;
    }
}
