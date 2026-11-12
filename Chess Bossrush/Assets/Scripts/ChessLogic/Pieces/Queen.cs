
using UnityEngine;

public class Queen : PieceLogic
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
                    base.Movement(i, j);
                }
            }
        }
        
    }

    
    
}
