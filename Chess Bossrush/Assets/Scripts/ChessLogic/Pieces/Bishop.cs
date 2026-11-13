
using UnityEngine;

public class Bishop : PieceLogic
{
    


    public override void UsePiece()
    {
        base.UsePiece();
        base.Movement(1,1);
        base.Movement(-1,1);
        base.Movement(1, -1);
        base.Movement(-1, -1);
        
    }

    
    
}
