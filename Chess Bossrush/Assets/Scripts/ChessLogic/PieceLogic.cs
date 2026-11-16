using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PieceLogic : MonoBehaviour
{
    public bool black;
    [SerializeField] protected BoardLogic boardLogic;
    
    public int xBase;
    public int yBase;

    void Start()
    {

        boardLogic = FindAnyObjectByType<BoardLogic>();

        Vector3 pos = transform.localPosition;
        pos.x = Mathf.Floor(transform.localPosition.x * 8 / 3);
        pos.y = Mathf.Floor(transform.localPosition.y * 8 / 3);

        xBase = (int)pos.x;
        xBase += 4;
        yBase = (int)pos.y;
        yBase += 4;
        if (GetComponent<Renderer>().material.color.r < 0.5f)
            black = true;

        boardLogic.boardState[xBase + yBase * 8] = this;
        
        pos *= 3f / 8f;
        pos += Vector3.one * 3f / 16f;
        pos.z = 0;
        transform.localPosition = pos;
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

        while (boardLogic.CheckIfValidMove(x, y, this))
        {
            x += xMove;
            y += yMove;
        }
        
    }

    public virtual void UpdatePos(Vector3 pos)
    {
        boardLogic.boardState[xBase + yBase * 8] = null;
        xBase = (int)pos.x + 4;
        yBase = (int)pos.z + 4;
        if (boardLogic.boardState[xBase + yBase * 8] != null)
            Destroy(boardLogic.boardState[xBase + yBase * 8].gameObject);
        boardLogic.boardState[xBase + yBase * 8] = this;
        transform.position = boardLogic.transform.GetChild(xBase + yBase * 8).position;
    }
}
