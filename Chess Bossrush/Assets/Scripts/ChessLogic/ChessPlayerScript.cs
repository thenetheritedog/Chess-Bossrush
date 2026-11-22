using UnityEngine;

public class ChessPlayerScript : MonoBehaviour
{
    [SerializeField] PieceLogic pieceUsing;
    [SerializeField] BoardLogic board;
    public Vector2 mousePos;

    public void FindPiece()
    {


        if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out RaycastHit hit, 5f))
        {
            if (hit.collider.GetComponent<PieceLogic>() != null)
            {
                PieceLogic hitPiece = hit.collider.GetComponent<PieceLogic>();
                
                if (pieceUsing != hitPiece && hitPiece.black == board.blackTurn)
                {
                    pieceUsing = hitPiece;
                    pieceUsing.UsePiece();
                }
                else if (pieceUsing != null)
                {
                    if (hitPiece.black != pieceUsing.black)
                    {
                        board.MovePiece(pieceUsing, hit.point);
                        pieceUsing = null;
                        board.RemoveAllLitUpSquares();
                    }
                    else
                    {
                        pieceUsing = null;
                        board.RemoveAllLitUpSquares();
                    }
                }
                else 
                { 
                    pieceUsing = null; 
                    board.RemoveAllLitUpSquares();
                }

            }
            else if (hit.collider.GetComponent<SelectUpgrade>() != null)
            {
                hit.collider.GetComponent<SelectUpgrade>().ClickOnUpgrade();
            }
            else
            {
                if (pieceUsing != null)
                    board.MovePiece(pieceUsing, hit.point);
                pieceUsing = null;
                board.RemoveAllLitUpSquares();
            }
        }
    }
}
