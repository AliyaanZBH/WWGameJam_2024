using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRoundAnswer : MonoBehaviour
{
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    public bool player1Correct = false, player2Correct = false;

    private void Update()
    {
        Puzzle_Controller player1Controller = player1.GetComponent<Puzzle_Controller>();
        Puzzle_Controller player2Controller = player2.GetComponent<Puzzle_Controller>();

        List<GameObject> player1Answer = player1Controller.GetAnswer();
        List<GameObject> player2Answer = player2Controller.GetAnswer();

        GridGenerator gridGen = grid.GetComponent<GridGenerator>();

        // in order bottom left, top right, top left bottom right
        List<GameObject> answer = gridGen.previousAnswer;

        // bottom left, top left
        if (player1Answer[0] == answer[0] && player1Answer[1] == answer[2])
        {
            Debug.Log("Player1 is correct\n");
            player1Correct = true;
        }

        // bottom right, top right
        if (player2Answer[0] == answer[3] && player2Answer[1] == answer[1])
        {
            Debug.Log("Player2 is correct\n");
            player2Correct = true;
        }

        if (player1Correct && player2Correct)
        {
            Debug.Log("Win round\n");
        }
    }
}
