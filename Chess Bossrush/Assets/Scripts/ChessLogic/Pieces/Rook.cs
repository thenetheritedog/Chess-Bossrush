
using UnityEngine;

public class Rook : PieceLogic
{
    


    public override void UsePiece()
    {
        base.UsePiece();
        base.Movement(1,0);
        base.Movement(-1,0);
        base.Movement(0, 1);
        base.Movement(0, -1);
        
    }

    
    
}
