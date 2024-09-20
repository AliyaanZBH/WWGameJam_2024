using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Controller : MonoBehaviour
{

    // How far to move along the grid - 1 cell at a time
    [SerializeField] private int movementDistance = 1;

    // Which player are we?
    [SerializeField] private int player = 1;

    // Get the game grid
    [SerializeField] GameObject gridObject;
    private GridGenerator gridGen;

    // Current transform of the player
    private Transform trans;

    // Vector direction to move the player along the grid
    private Vector3 newPos;

    // Adjusted value to account for scale of player rect
    private int adjustedGridPos;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        gridGen = gridObject.GetComponent<GridGenerator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player == 1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                // Check we aren't moving outside the grid
                // Adjust by 1.5 as the rect is 2 units tall on Y-Axis, but in order to sit at the start of the grid it also needs to be adjust by .5
                adjustedGridPos = (int)(trans.position.y + 1.5f);
                if (adjustedGridPos < gridGen.GetGrid().y)
                    newPos = new Vector3(0, movementDistance, 0);
                else
                    return;
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                adjustedGridPos = (int)(trans.position.y - 0.5f);
                if (adjustedGridPos > 0)
                    newPos = new Vector3(0, -movementDistance, 0);
                else
                    return;
            }  
            
            else if (Input.GetKeyDown(KeyCode.A))
            {
                // No need to adjust for the X axis
                if (trans.position.x > 0)
                    newPos = new Vector3(-movementDistance, 0, 0);
                else
                    return;
            }

            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (trans.position.x < gridGen.GetGrid().x - 1)
                    newPos = new Vector3(movementDistance, 0, 0);
                else
                    return;
            }

            // 0 out movement
            else
            {
                newPos = new Vector3(0, 0, 0);

            }


            // Check puzzle answer
        }
        
        // Repeat for player 2
        if (player == 2)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                adjustedGridPos = (int)(trans.position.y + 1.5f);
                if (adjustedGridPos < gridGen.GetGrid().y)
                    newPos = new Vector3(0, movementDistance, 0);
                else
                    return;
            }

            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                adjustedGridPos = (int)(trans.position.y - 0.5f);
                if (adjustedGridPos > 0)
                    newPos = new Vector3(0, -movementDistance, 0);
                else
                    return;
            }

            else if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (trans.position.x > 0)
                    newPos = new Vector3(-movementDistance, 0, 0);
                else
                    return;
            }

            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (trans.position.x < gridGen.GetGrid().x - 1)
                    newPos = new Vector3(movementDistance, 0, 0);
                else
                    return;
            }

            else
            {
                newPos = new Vector3(0, 0, 0);
            }
        }
        
        // Update position
        trans.position += newPos;

    }
}
