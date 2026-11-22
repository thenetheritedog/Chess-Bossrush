using UnityEngine;
using UnityEngine.InputSystem;

public class InputChess : MonoBehaviour
{

    ChessPlayerScript player;
    BoardLogic boardLogic;
    void Start()
    {
        player = GetComponent<ChessPlayerScript>();
        boardLogic = FindAnyObjectByType<BoardLogic>();
    }

    // Update is called once per frame
    
    public void OnPoint(InputValue inputVal)
    {
        player.mousePos = inputVal.Get<Vector2>();

    }

    public void OnClick(InputValue inputVal)
    {
        if (inputVal.Get<float>() > 0.5f)
            player.FindPiece();

    }

    //public void OnJump(InputValue inputVal)
    //{
    //    boardLogic.EndGame();
    //}

    //public void OnPrevious(InputValue inputVal)
    //{
    //    boardLogic.FlipBoardToFEN();
    //}

    //public void OnNext(InputValue inputVal)
    //{
    //    boardLogic.FlipBoardToFEN();
    //    boardLogic.FlipBoardToFEN();
    //}
}
