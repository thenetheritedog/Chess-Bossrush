
using System.Xml.Linq;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class Knight : PieceLogic
{

    public override void UsePiece()
    {
        base.UsePiece();
        
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -1; j <= 1; j += 2)
            {
                Movement(2 * i, j);
                Movement(i, 2 * j);
            }
        }

    }

    protected override void Movement(int xMove, int yMove)
    {
        int x = xBase + xMove;
        int y = yBase + yMove;

        boardLogic.CheckIfValidMove(x, y, this);

    }
    public override bool CanCheck(int xMove, int yMove, bool oneAway, bool blackPiece)
    {
        return blackPiece != black && Mathf.Abs(xMove) + Mathf.Abs(yMove) > 2;
    }
}
