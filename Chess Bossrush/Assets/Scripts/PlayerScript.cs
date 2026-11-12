using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] PieceLogic pieceUsing;
    [SerializeField] BoardLogic board;
    public Vector2 mousePos;

    public void FindPiece()
    {
        
        Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out RaycastHit hit);
        Debug.DrawRay(Camera.main.ScreenPointToRay(mousePos).origin,Camera.main.ScreenPointToRay(mousePos).direction, color:Color.red, 5f);
        if (hit.collider.GetComponent<PieceLogic>() != null)
        {
            if (pieceUsing != hit.collider.GetComponent<PieceLogic>())
            {
                pieceUsing = hit.collider.GetComponent<PieceLogic>();
                pieceUsing.UsePiece();
            }
            else { pieceUsing = null; }
            
        }
        else
        {
            pieceUsing = null;
        }
        
    }
}
