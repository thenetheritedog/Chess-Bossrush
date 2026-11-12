using UnityEngine;

public class PieceLogic : MonoBehaviour
{
    protected BoardLogic boardLogic;

    int xBase;
    int yBase;

    private void Start()
    {
        boardLogic = FindAnyObjectByType<BoardLogic>();
        Vector3 pos = transform.localPosition;
        pos.x = Mathf.Round((transform.localPosition.x + 3/8) * 4/3) * 3/4;
        pos.x -= 3 / 8;
        pos.z = Mathf.Round((transform.localPosition.z+3/8) * 4/3) * 3/4;
        pos.z -= 3 / 8;
        transform.localPosition = pos;
        xBase = (int)(transform.localPosition.x * 8 / 3);
        xBase += 4;
        yBase = (int)(transform.localPosition.z * 8 / 3);
        yBase += 4;
        boardLogic.boardState[xBase, yBase] = this;
        Debug.Log($"{xBase},{yBase}");
    }
    public virtual void UsePiece()
    {
        boardLogic.RemoveAllLitUpSquares();
    }
    protected virtual void Movement(int xMove, int yMove)
    {
        int x = xBase + xMove;
        int y = yBase + yMove;
        if (xMove == 0 && yMove == 0)
            return;

        while (boardLogic.CheckIfValidMove(x, y))
        {
            x += xMove;
            y += yMove;

        }
        
    }
}
