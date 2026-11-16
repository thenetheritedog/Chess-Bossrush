
using Unity.VisualScripting;
using UnityEngine;

public class Rook : PieceLogic
{
    public bool hasMoved = false;


    public override void UsePiece()
    {
        base.UsePiece();

        base.Movement(1, 0);
        base.Movement(-1, 0);
        base.Movement(0, 1);
        base.Movement(0, -1);

    }


    public override void UpdatePos(Vector3 pos)
    {
        base.UpdatePos(pos);
        hasMoved = true;
    }
}
