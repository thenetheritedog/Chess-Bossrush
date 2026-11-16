
using System.Collections;

using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class BoardLogic : MonoBehaviour
{
    public PieceLogic[] boardState = new PieceLogic[64];
    public King whiteKing;
    public King blackKing;

    [SerializeField] GameObject litSquareInst;
    [SerializeField] GameObject upgradeShow;
    [SerializeField] Canvas endGameScreen;
    [SerializeField] TextMeshProUGUI winText;

    bool blackTurn = false;
    bool checkForLegalMoves;

    int xUpgrade;
    int yUpgrade;

    Vector3 upgradeShowDefaultPlace;

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
        upgradeShowDefaultPlace = upgradeShow.transform.position;
        upgradeShow.transform.position = upgradeShowDefaultPlace + Vector3.left * 10;

    }
    public bool CheckIfValidMove(int xCord, int yCord, PieceLogic piece)
    {
        upgradeShow.transform.position = upgradeShowDefaultPlace + Vector3.left * 10;
        if (piece != whiteKing)
            whiteKing.ResetKingPos();
        if (piece != blackKing)
            blackKing.ResetKingPos();

        if (xCord > 7 || yCord > 7 || xCord < 0 || yCord < 0)
            return false;
        if (piece.black != blackTurn)
            return false;

        PieceLogic[] newPos = (PieceLogic[])boardState.Clone();
        newPos[piece.xBase + piece.yBase * 8] = null;
        newPos[xCord + yCord * 8] = piece;
        bool kingNotInDanger = true;
        if (piece.black)
        {
            if (blackKing.CheckKingDanger(newPos))
            { kingNotInDanger = false; }
        }
        else if (whiteKing.CheckKingDanger(newPos))
        { kingNotInDanger = false; }


        GameObject lightUp = transform.GetChild(xCord + yCord * 8).gameObject;
        if (boardState[xCord + yCord * 8] != null)
        {
            if (piece is Pawn || boardState[xCord + yCord * 8].yBase == yCord) 
            { 
                if (piece is Pawn && piece.xBase == xCord)
                    return false;

                if (boardState[xCord + yCord * 8].black != piece.black && kingNotInDanger)
                {        
                    checkForLegalMoves = false;
                    lightUp.GetComponent<Image>().color = Color.red;   
                }

                return false;
            }
        }

        if (piece is Pawn && piece.xBase != xCord)
            return false;

        if (kingNotInDanger)
        {
            lightUp.GetComponent<Image>().color = Color.white;
            checkForLegalMoves = false;
        }
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
        if (localPos.x > 3 || localPos.z > 3 || localPos.x < -4 || localPos.z < -4)
            return;
        GameObject lightUp = transform.GetChild((int)localPos.x + 4 + ((int)localPos.z +4) * 8).gameObject;
        if (lightUp.GetComponent<Image>().color != Color.clear)
        {
            if (piece is Pawn && (localPos.z == 3 || localPos.z == -4))
            {
                yUpgrade = (int)localPos.z + 4;
                xUpgrade = (int)localPos.x + 4;
                upgradeShow.transform.position = upgradeShowDefaultPlace;
                return;
            }
            piece.UpdatePos(localPos);
            blackTurn = !blackTurn;
            RemoveAllLitUpSquares();
            MoveCheckAfterTurn();
        }
        else
        {
            RemoveAllLitUpSquares();
        }
    }

    void MoveCheckAfterTurn()
    {
        checkForLegalMoves = true;
        foreach (PieceLogic pieceLogic in boardState)
        {
            if (pieceLogic == null)
                continue;
            if (pieceLogic.black != blackTurn)
                continue;
            pieceLogic.UsePiece();
        }
        RemoveAllLitUpSquares();
        if (checkForLegalMoves == true)
            EndGame();
    }

    public void EndGame()
    {
        if (whiteKing.CheckKingDanger(boardState))
        {
            endGameScreen.enabled = true;
            winText.text = "Black wins!";
        }
        else if (blackKing.CheckKingDanger(boardState))
        {
            endGameScreen.enabled = true;
            winText.text = "White wins!";
        }
        else
        {
            endGameScreen.enabled = true;
            winText.text = "Stalemate";
        }
    }

    

    public void SetupNewPiece(PieceLogic prefab)
    {
        if (yUpgrade == 0)
            Destroy(boardState[xUpgrade + 8].gameObject);
        else
            Destroy(boardState[xUpgrade + 48].gameObject);
        PieceLogic pieceUpg = Instantiate(prefab);
        pieceUpg.transform.parent = transform.parent.parent;
        pieceUpg.transform.position = transform.GetChild(xUpgrade + yUpgrade * 8).position;
        pieceUpg.transform.position += 0.15f * Vector3.up;
        pieceUpg.black = yUpgrade == 0 ? true : false;
        upgradeShow.transform.position = upgradeShowDefaultPlace - Vector3.right * 10;
        blackTurn = !blackTurn;
    }


    public static void RematchGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}