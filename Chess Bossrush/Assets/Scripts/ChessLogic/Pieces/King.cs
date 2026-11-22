
using System.Xml.Linq;
using UnityEngine.UI;
using UnityEngine;

public class King : PieceLogic
{
    int kingMovedX;
    int kingMovedY;
    public bool hasMoved = false;

    public override void UsePiece()
    {
        base.UsePiece();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Movement(i, j);
            }
        }
        kingMovedX = xBase;
        kingMovedY = yBase;
    }

    protected override void Movement(int xMove, int yMove)
    {
        int x = xBase + xMove;
        int y = yBase + yMove;

        kingMovedX = x;
        kingMovedY = y;

        if (xMove == 0 && yMove == 0)
            return;

        if (boardLogic.CheckIfValidMove(x, y, this) && !hasMoved && yMove == 0)
        {
            CastleCheck();
        }
    }

    void CastleCheck()
    {
        kingMovedX = xBase;
        kingMovedY = yBase;
        if (CheckKingDanger(boardLogic.boardState))
            return;
        if (boardLogic.boardState[yBase * 8] is Rook rook)
        {
            if (!rook.hasMoved)
            {
                kingMovedX = 2;
                boardLogic.CheckIfValidMove(kingMovedX, yBase, this);
            }
        }
        if (boardLogic.boardState[7 +yBase * 8] is Rook rook2)
        {
            if (!rook2.hasMoved)
            {
                kingMovedX = 6;
                boardLogic.CheckIfValidMove(kingMovedX, yBase, this);
            }
        }
    }


    public bool CheckKingDanger(PieceLogic[] pieces)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (Checks(i, j, pieces))
                {
                    return true;
                }
            }
        }
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -1; j <= 1; j += 2)
            {
                if (Checks(2 * i, j, pieces) || Checks(i, 2 * j, pieces))
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool Checks(int xMove, int yMove, PieceLogic[] pieces)
    {

        if (xMove == 0 && yMove == 0)
            return false;

        int xPos = kingMovedX + xMove;
        int yPos = kingMovedY + yMove;

        bool hitValid = false;
        bool moved1Square = Mathf.Abs(xMove) + Mathf.Abs(yMove) < 3;
        
        PieceLogic pieceHit = null;

        while (!hitValid && !(xPos > 7 || yPos > 7 || xPos < 0 || yPos < 0))
        {
            if (pieces[xPos + yPos * 8] != null && pieces[xPos + yPos * 8] != this)
            {
                hitValid = true;
                pieceHit = pieces[xPos + yPos * 8];
            }
            else if (Mathf.Abs(xMove) + Mathf.Abs(yMove) > 2)
                break;
            else
            {
                moved1Square = false;
                xPos += xMove;
                yPos += yMove;
            }

        }


        if (hitValid)
            return pieceHit.CanCheck(xMove,yMove,moved1Square,black);
        return false;
    }

    public void ResetKingPos()
    {
        kingMovedX = xBase;
        kingMovedY = yBase;
    }

    public override void UpdatePos(Vector3 pos)
    {
        base.UpdatePos(pos);
        if (!hasMoved && Mathf.Abs(4-xBase) > 1)
        {
            if (xBase < 4)
                boardLogic.boardState[yBase * 8].UpdatePos(new Vector3(-1,0, yBase - 4));
            
            else
                boardLogic.boardState[7 + yBase * 8].UpdatePos(new Vector3(1, 0, yBase - 4));
        }
        hasMoved = true;
    }

    public override bool CanCheck(int xMove, int yMove, bool oneAway, bool blackPiece)
    {
        return oneAway && blackPiece != black;
    }
}
