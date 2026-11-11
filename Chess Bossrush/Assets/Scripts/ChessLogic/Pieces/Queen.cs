using UnityEngine;

public class Queen : PieceLogic
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UsePiece()
    {
        transform.position += transform.up;
    }
}
