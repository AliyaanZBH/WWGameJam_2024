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
    private int adjustedGridPosX;
    private int adjustedGridPosY;

    public List<GameObject> Answer;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        gridGen = gridObject.GetComponent<GridGenerator>();

        Answer = new List<GameObject>();
    }


    public  List<GameObject> GetAnswer() { return Answer; }

    // Update is called once per frame
    void Update()
    {

        if (player == 1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                // Check we aren't moving outside the grid
                // Adjust by 1.5 as the rect is 2 units tall on Y-Axis, but in order to sit at the start of the grid it also needs to be adjust by .5
                adjustedGridPosY = (int)(trans.position.y + 1.5f);
                if (adjustedGridPosY < gridGen.GetGrid().y)
                    newPos = new Vector3(0, movementDistance, 0);
                else
                    return;
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                adjustedGridPosY = (int)(trans.position.y - 0.5f);
                if (adjustedGridPosY > 0)
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


            // Generate puzzle answer

            if (Input.GetKeyDown(KeyCode.G))
            {
                Answer.Clear();


                adjustedGridPosY = (int) (trans.position.y - 0.5f);
                // Find out where we are on the grid
                // Use adjusted value for Y coordinate, can use actual trans value for X
                for (int i = 0; i < gridGen.GetGrid().cells.Count; i++)
                {
                    // Find the bottom cell that matches our current position
                    if (gridGen.GetGrid().cells[i].x == trans.position.x && gridGen.GetGrid().cells[i].y == adjustedGridPosY)
                    {
                        // Add the bottom fruit
                        Answer.Add(gridGen.GetGrid().cells[i].fruit);
                    }
                }
                // Repeat for fruit above
                for (int i = 0; i < gridGen.GetGrid().cells.Count; i++)
                {
                    // Find the bottom cell that matches our current position
                    if (gridGen.GetGrid().cells[i].x == trans.position.x && gridGen.GetGrid().cells[i].y == adjustedGridPosY + 1)
                    {
                        // Add the next fruit
                        Answer.Add(gridGen.GetGrid().cells[i].fruit);
                    }
                }

                //GameObject tmp = Answer[0];
                //Answer[0] = Answer[1];
                //Answer[1]  = tmp; ;
            }
        }
        
        // Repeat for player 2
        if (player == 2)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                adjustedGridPosY = (int)(trans.position.y + 1.5f);
                if (adjustedGridPosY < gridGen.GetGrid().y)
                    newPos = new Vector3(0, movementDistance, 0);
                else
                    return;
            }

            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                adjustedGridPosY = (int)(trans.position.y - 0.5f);
                if (adjustedGridPosY > 0)
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

            if (Input.GetKeyDown(KeyCode.P))
            {
                Answer.Clear();

                adjustedGridPosY = (int)(trans.position.y - 0.5f);

                // Find out where we are on the grid
                // Use adjusted value for Y coordinate, can use actual trans value for X
                for (int i = 0; i < gridGen.GetGrid().cells.Count; i++)
                {
                    // Find the bottom cell that matches our current position
                    if (gridGen.GetGrid().cells[i].x == trans.position.x && gridGen.GetGrid().cells[i].y == adjustedGridPosY)
                    {
                        // Add the bottom fruit
                        Answer.Add(gridGen.GetGrid().cells[i].fruit);
                    }
                }
                // Repeat for fruit above
                for (int i = 0; i < gridGen.GetGrid().cells.Count; i++)
                {
                    // Find the bottom cell that matches our current position
                    if (gridGen.GetGrid().cells[i].x == trans.position.x && gridGen.GetGrid().cells[i].y == adjustedGridPosY + 1)
                    {
                        // Add the next fruit
                        Answer.Add(gridGen.GetGrid().cells[i].fruit);
                    }
                }

                //GameObject tmp = Answer[0];
                //Answer[0] = Answer[1];
                //Answer[1] = tmp; ;
            }
        }
        
        // Update position
        trans.position += newPos;

    }
}
