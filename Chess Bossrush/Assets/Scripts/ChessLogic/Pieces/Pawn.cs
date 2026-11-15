
using System.Xml.Linq;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class Pawn : PieceLogic
{
    int moveForward;

    public override void UsePiece()
    {

        moveForward = black ? -1 : 1;
        base.UsePiece();

        if (boardLogic.boardState[xBase + (yBase - moveForward) * 8] == this)
            boardLogic.boardState[xBase + (yBase - moveForward) * 8] = null;

        
        for (int i = -1; i <= 1; i++)
        {
            Movement(i, moveForward);
            if (i == 0 && (yBase == 1 || yBase == 6))
            {
                Movement(0, 2 * moveForward);
            }
        }
        

    }

    protected override void Movement(int xMove, int yMove)
    {
        int x = xBase + xMove;
        int y = yBase + yMove;

        boardLogic.CheckIfValidMove(x, y, this);

    }

    public override void UpdatePos(Vector3 pos)
    {
        int yBaseOld = yBase;
        base.UpdatePos(pos);
        if (Mathf.Abs(yBaseOld - yBase) == 2)
        {
            boardLogic.boardState[xBase + (yBase - moveForward) * 8] = this;
        }
    }

}
