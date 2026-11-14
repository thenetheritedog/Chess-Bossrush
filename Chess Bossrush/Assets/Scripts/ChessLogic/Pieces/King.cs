
using System.Xml.Linq;
using UnityEngine;

public class King : PieceLogic
{


    public override void UsePiece()
    {
        base.UsePiece();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i != 0 || j != 0)
                {
                    Movement(i, j);
                }
            }
        }

    }

    protected override void Movement(int xMove, int yMove)
    {
        int x = xBase + xMove;
        int y = yBase + yMove;
        if (xMove == 0 && yMove == 0)
            return;

        boardLogic.CheckIfValidMove(x, y, black, this);

    }


    public bool CheckKingDanger(PieceLogic[,] pieces)
    {
        if (UpDownLeftRightCheck(1,0,pieces) || UpDownLeftRightCheck(-1, 0, pieces) || UpDownLeftRightCheck(0, 1, pieces) || UpDownLeftRightCheck(0, -1, pieces))
            return true;
        return false;
    }

    bool UpDownLeftRightCheck(int xMove, int yMove, PieceLogic[,] pieces)
    {
        bool hitValid = false;
        int xPos = xBase;
        int yPos = yBase;
        while (!hitValid && !( xPos > 6 || yPos > 6 || xPos < 1 || yPos < 1))
        {
            xPos += xMove;
            yPos += yMove;
            if (pieces[xPos,yPos] != null)
                hitValid = true;
        }
        if (hitValid && (pieces[xPos, yPos] is Queen || pieces[xPos, yPos] is Rook))
        {
            return pieces[xPos,yPos].black != black;
        }
        return false;
    }
}
