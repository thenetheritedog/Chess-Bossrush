
using UnityEngine;
using UnityEngine.UI;

public class BoardLogic : MonoBehaviour
{
    public PieceLogic[,] boardState = new PieceLogic[8, 8];
    public King whiteKing;
    public King blackKing;
    [SerializeField] GameObject litSquareInst;

    private void Start()
    {
        for (int i = 0; i < 64; i++)
        {
            Instantiate(litSquareInst, transform);
        }
        King[] kings = FindObjectsByType<King>(FindObjectsSortMode.InstanceID);
        foreach (King king in kings) 
        {
            if (king.black)
            {
                blackKing = king;
            }
            else
            {
                whiteKing = king;
            }
        }

    }
    public bool CheckIfValidMove(int xCord, int yCord, bool black, PieceLogic piece)
    {

        if (xCord > 7 || yCord > 7 || xCord < 0 || yCord < 0)
            return false;
        PieceLogic[,] newPos = boardState;
        newPos[piece.xBase, piece.yBase] = null;
        newPos[xCord, yCord] = piece;
        if (black)
        {
            if (blackKing.CheckKingDanger(newPos)) { return true; }
        }
        else if (whiteKing.CheckKingDanger(newPos)) { return true; }
            

        GameObject lightUp = transform.GetChild(xCord + yCord * 8).gameObject;
        if (boardState[xCord,yCord] != null)
        {
            if (boardState[xCord, yCord].black != black)
                lightUp.GetComponent<Image>().color = Color.red;

            return false;
        }
        
        
        
        lightUp.GetComponent<Image>().color = Color.white;
        
        return true;
        
        
    }

    public void RemoveAllLitUpSquares()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.clear;

        }
    }

    public void MovePiece(PieceLogic piece, Vector3 pos)
    {
        Vector3 localPos = pos - transform.position;
        localPos.x = Mathf.Floor(localPos.x * 8 / 3);
        localPos.z = Mathf.Floor(localPos.z * 8 / 3);
        GameObject lightUp = transform.GetChild((int)localPos.x + 4 + ((int)localPos.z +4) * 8).gameObject;
        if (lightUp.GetComponent<Image>().color != Color.clear)
        {
            piece.UpdatePos(localPos);
            RemoveAllLitUpSquares();
        }
        else
        {
            RemoveAllLitUpSquares();
        }
    }
}
