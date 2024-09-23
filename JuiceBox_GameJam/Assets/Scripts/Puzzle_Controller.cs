using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using static DrawRoundAnswer;
using static GridGenerator;

public class Puzzle_Controller : MonoBehaviour
{

    // How far to move along the grid - 1 cell at a time
    [SerializeField] private int movementDistance = 1;

    // Which player are we?
    [SerializeField] private int player = 1;

    [SerializeField] private Color BaseColour;
    [SerializeField] private Color SubmittedColour;

    // Get the game grid
    [SerializeField] GameObject gridObject;
    private GridGenerator gridGen;

    // Current transform of the player
    private Transform trans;

    // Player sprite
    private SpriteRenderer spr;

    // Vector direction to move the player along the grid
    private Vector3 newPos;

    // Adjusted value to account for scale of player rect, giving us a clearer grid position when determining answer
    private int adjustedGridPosY;

    public List<EMyFruit> Answer;

    public bool bAnswerLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        spr = GetComponent<SpriteRenderer>();
        spr.color = BaseColour;

        // Wait until the grid becomes valied
        Assert.IsNotNull(gridObject);
        gridGen = gridObject.GetComponent<GridGenerator>();

        Answer = new List<EMyFruit>();
    }


    public  List<EMyFruit> GetAnswer() { return Answer; }

    // To be called from the answer manager to release the lock and also change the color
    public void UnlockAnswer()
    {
        bAnswerLocked = false;
        spr.color = BaseColour;
    }
    // Input actions

    void IA_MoveUp()
    {
        // Check we aren't moving outside the grid
        // Adjust by 1.5 as the rect is 2 units tall on Y-Axis, but in order to sit at the start of the grid it also needs to be adjust by .5
        adjustedGridPosY = (int)(trans.position.y + 1.5f);
        if (adjustedGridPosY < gridGen.GetGrid().y)
            newPos = new Vector3(0, movementDistance, 0);
        else
            return;
    }

    void IA_MoveDown()
    {
        adjustedGridPosY = (int)(trans.position.y - 0.5f);
        if (adjustedGridPosY > 0)
            newPos = new Vector3(0, -movementDistance, 0);
        else
            return;
    }

    void IA_MoveLeft()
    {
        // No need to adjust for the X axis
        if (trans.position.x > 0)
            newPos = new Vector3(-movementDistance, 0, 0);
        else
            return;
    }
    void IA_MoveRight()
    {
        if (trans.position.x < gridGen.GetGrid().x - 1)
            newPos = new Vector3(movementDistance, 0, 0);
        else
            return;
    }

    void IA_SubmitAnswer()
    {
        // Check that we haven't submitted already
        if (!bAnswerLocked)
        {
            adjustedGridPosY = (int)(trans.position.y - 0.5f);
            // Find out where we are on the grid
            // Use adjusted value for Y coordinate, can use actual trans value for X
            for (int i = 0; i < gridGen.GetGrid().cells.Count; i++)
            {
                // Find the bottom cell that matches our current position
                if (gridGen.GetGrid().cells[i].x == trans.position.x && gridGen.GetGrid().cells[i].y == adjustedGridPosY)
                {
                    // Add the bottom fruit
                    Answer.Add(gridGen.GetGrid().cells[i].name);
                }
            }
            // Repeat for fruit above
            for (int i = 0; i < gridGen.GetGrid().cells.Count; i++)
            {
                // Find cell directly above the last one by simply adding 1 to the adjustedPos variable
                if (gridGen.GetGrid().cells[i].x == trans.position.x && gridGen.GetGrid().cells[i].y == adjustedGridPosY + 1)
                {
                    // Add the next fruit
                    Answer.Add(gridGen.GetGrid().cells[i].name);
                }
            }

            // "Lock in" answer for the player, restrict movement and perhaps change something elsewhere
            bAnswerLocked = true;

            // Change player sprite colour to indicate the change to them
            spr.color = SubmittedColour;

        }
        else // Un-lock the players input if there is still time left
        {
            // Reset answer aswell
            Answer.Clear();
            UnlockAnswer();
        }
    }

    void HandleInput(int player)
    {
        // Handle movement first
        // Only check movement when we haven't locked an answer yet
        if (!bAnswerLocked)
        {
            // Setup input possibilities
            KeyCode up = KeyCode.None, down = KeyCode.None,
                    left = KeyCode.None, right = KeyCode.None;

            if (player == 1)
            {
                up = KeyCode.W; down = KeyCode.S;
                left = KeyCode.A; right = KeyCode.D;
            }
            // Repeat for player 2
            else if (player == 2)
            {
                up = KeyCode.UpArrow; down = KeyCode.DownArrow;
                left = KeyCode.LeftArrow; right = KeyCode.RightArrow;
            }

            // Actually handle input for both players!
            if (Input.GetKeyDown(up))
            {
                IA_MoveUp();
            }

            else if (Input.GetKeyDown(down))
            {
                IA_MoveDown();
            }

            else if (Input.GetKeyDown(left))
            {
                IA_MoveLeft();
            }

            else if (Input.GetKeyDown(right))
            {
                IA_MoveRight();
            }

            // 0 out movement
            else
            {
                newPos = new Vector3(0, 0, 0);

            }

  
        }

        // Handle other input actions
        KeyCode submitAnswer = KeyCode.None;
        if (player == 1)
            submitAnswer = KeyCode.G;
        else if (player == 2)
            submitAnswer = KeyCode.P;

        // Generate puzzle answer
        if (Input.GetKeyDown(submitAnswer))
        {
            Answer.Clear();
            IA_SubmitAnswer();

        }
    }

    // Update is called once per frame
    void Update()
    {

        // Refresh gridObject incase we didn't get it at the start
        if (gridGen == null)
        {
            gridGen = gridObject.GetComponent<GridGenerator>();
        }


        HandleInput(player);       

        // Update position
        trans.position += newPos;

    }
}
