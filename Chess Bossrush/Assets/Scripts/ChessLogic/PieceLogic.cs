using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PieceLogic : MonoBehaviour
{
    [SerializeField] protected bool black;
    protected BoardLogic boardLogic;
    
    int xBase;
    int yBase;

    private void Start()
    {
        boardLogic = FindAnyObjectByType<BoardLogic>();

        Vector3 pos = transform.localPosition;
        pos.x = Mathf.Floor(transform.localPosition.x * 8 / 3);
        pos.z = Mathf.Floor(transform.localPosition.z * 8 / 3);

        xBase = (int)pos.x;
        xBase += 4;
        yBase = (int)pos.z;
        yBase += 4;
        boardLogic.boardState[xBase, yBase] = this;
        pos *= 3f / 8f;
        pos.x += 3f / 16f;
        pos.z += 3f / 16f;
        pos.y = 0.15f;
        transform.localPosition = pos;
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

    public void UpdatePos(Vector3 pos)
    {
        boardLogic.boardState[xBase, yBase] = null;
        xBase = (int)pos.x + 4;
        yBase = (int)pos.z + 4;
        boardLogic.boardState[xBase, yBase] = this;
        pos *= 3f / 8f;
        pos.x += 3f / 16f;
        pos.z += 3f / 16f;
        pos.y = 0.15f;
        transform.localPosition = pos;
    }
}
