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

        if (player1Answer.Count != 0 && player2Answer.Count != 0)
        {

            // bottom left, top left
            if (player1Answer[0].GetComponent<SpriteRenderer>().sprite == answer[0].GetComponent<SpriteRenderer>().sprite && player1Answer[1].GetComponent<SpriteRenderer>().sprite == answer[2].GetComponent<SpriteRenderer>().sprite)
            {
                Debug.Log("Player1 is correct\n");
                player1Correct = true;
            }

            // bottom right, top right
            if (player2Answer[0].GetComponent<SpriteRenderer>().sprite == answer[3].GetComponent<SpriteRenderer>().sprite && player2Answer[1].GetComponent<SpriteRenderer>().sprite == answer[1].GetComponent<SpriteRenderer>().sprite)
            {
                Debug.Log("Player2 is correct\n");
                player2Correct = true;
            }

            if (player1Correct && player2Correct)
            {
                Debug.Log("Win round\n");
                gridGen.ResetGrid();
                gridGen.GenerateGrid();

                answer.Clear();
                player1Answer.Clear();
                player2Answer.Clear();
                player1Correct = false;
                player2Correct = false;

            }
        }
    }
}
