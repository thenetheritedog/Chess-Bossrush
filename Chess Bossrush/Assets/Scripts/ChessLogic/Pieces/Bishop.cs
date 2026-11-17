
using UnityEngine;

public class Bishop : PieceLogic
{



    public override void UsePiece()
    {
        base.UsePiece();

        base.Movement(1, 1);
        base.Movement(-1, 1);
        base.Movement(1, -1);
        base.Movement(-1, -1);

        
    }

    public override bool CanCheck(int xMove, int yMove, bool oneAway, bool blackPiece)
    {
        return blackPiece != black && Mathf.Abs(xMove) + Mathf.Abs(yMove) == 2;
    }

}
