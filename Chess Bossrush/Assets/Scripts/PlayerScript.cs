using UnityEngine;

public class PlayerScript : MonoBehaviour
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
                if (pieceUsing != hit.collider.GetComponent<PieceLogic>())
                {
                    pieceUsing = hit.collider.GetComponent<PieceLogic>();
                    pieceUsing.UsePiece();
                }
                else 
                { 
                    pieceUsing = null; 
                    board.RemoveAllLitUpSquares();
                }

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
